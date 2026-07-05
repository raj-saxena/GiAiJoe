#!/usr/bin/env python3
"""
Validates Unity .unity/.prefab YAML files for structural corruption that the
Unity Editor will not catch until it hangs or misbehaves at scene-load time:

  - Two Transform components claiming the same GameObject
  - A GameObject's own Transform component not matching what the GameObject
    thinks its Transform is
  - Cycles in the Transform parent chain
  - m_Father / m_Children references to fileIDs that don't exist in the file

Usage: validate_scene_hierarchy.py <file.unity> [<file.unity> ...]
Exits non-zero if any file fails validation.
"""
import re
import sys

DOC_HEADER_RE = re.compile(r"^(\d+) &(-?\d+)")


def parse_documents(text):
    docs = []
    for chunk in text.split("--- !u!")[1:]:
        header_line = chunk.split("\n", 1)[0]
        m = DOC_HEADER_RE.match(header_line)
        if not m:
            continue
        doc_type, file_id = m.group(1), m.group(2)
        docs.append((doc_type, file_id, chunk))
    return docs


def parse_fileid_field(chunk, field_name):
    m = re.search(re.escape(field_name) + r": \{fileID: (-?\d+)", chunk)
    return m.group(1) if m else None


def parse_fileid_list(chunk, field_name):
    m = re.search(re.escape(field_name) + r":\n((?:\s*- \{fileID: -?\d+\}\n?)*)", chunk)
    if not m:
        return []
    return re.findall(r"\{fileID: (-?\d+)\}", m.group(1))


def validate(path, text):
    errors = []
    docs = parse_documents(text)
    all_ids = {file_id for _, file_id, _ in docs}

    gameobjects = {}  # file_id -> list of component fileIDs
    transforms = {}   # file_id -> {gameobject, father, children}

    for doc_type, file_id, chunk in docs:
        if doc_type == "1":  # GameObject
            component_ids = parse_fileid_list(chunk, "m_Component") or re.findall(
                r"- component: \{fileID: (-?\d+)\}", chunk
            )
            gameobjects[file_id] = component_ids
        elif doc_type == "4":  # Transform
            transforms[file_id] = {
                "gameobject": parse_fileid_field(chunk, "m_GameObject"),
                "father": parse_fileid_field(chunk, "m_Father"),
                "children": parse_fileid_list(chunk, "m_Children"),
            }

    # Check 1: no two Transforms claim the same GameObject
    go_to_transforms = {}
    for tid, t in transforms.items():
        go_to_transforms.setdefault(t["gameobject"], []).append(tid)
    for go, tids in go_to_transforms.items():
        if len(tids) > 1:
            errors.append(
                f"GameObject {go} has {len(tids)} Transform components: {tids} "
                "(a GameObject must have exactly one Transform)"
            )

    # Check 2: GameObject's own Transform (from m_Component) matches a Transform
    # that actually claims that GameObject.
    for go_id, component_ids in gameobjects.items():
        component_transforms = [cid for cid in component_ids if cid in transforms]
        for tid in component_transforms:
            if transforms[tid]["gameobject"] != go_id:
                errors.append(
                    f"GameObject {go_id} lists Transform {tid} as its component, "
                    f"but that Transform's m_GameObject is {transforms[tid]['gameobject']}"
                )

    # Check 3: no cycles in the parent chain, and father references are valid
    for tid, t in transforms.items():
        seen = set()
        cur = tid
        while cur and cur != "0":
            if cur in seen:
                errors.append(f"Transform {tid} has a cyclic parent chain (revisits {cur})")
                break
            if cur != tid and cur not in transforms:
                errors.append(f"Transform {tid}'s ancestor {cur} does not exist in this file")
                break
            seen.add(cur)
            cur = transforms.get(cur, {}).get("father")

    # Check 4: m_Children entries reference fileIDs that exist and are Transforms
    for tid, t in transforms.items():
        for child in t["children"]:
            if child not in transforms:
                errors.append(
                    f"Transform {tid} lists child {child}, which is not a Transform in this file"
                )

    return errors


def main(argv):
    if len(argv) < 2:
        print(__doc__)
        return 1

    had_errors = False
    for path in argv[1:]:
        with open(path, "r", encoding="utf-8", errors="replace") as f:
            text = f.read()
        errors = validate(path, text)
        if errors:
            had_errors = True
            print(f"FAIL: {path}")
            for e in errors:
                print(f"  - {e}")
        else:
            print(f"OK: {path}")

    return 1 if had_errors else 0


if __name__ == "__main__":
    sys.exit(main(sys.argv))
