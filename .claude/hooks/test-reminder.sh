#!/bin/bash
# Stop hook: cheap, non-blocking reminder. Never blocks the turn (always exit 0).
cd "$CLAUDE_PROJECT_DIR" 2>/dev/null || exit 0

changed=$(git status --porcelain -- Assets/Scripts Assets/Tests 2>/dev/null)
scripts_changed=$(echo "$changed" | grep -E 'Assets/Scripts/.*\.cs$')
tests_changed=$(echo "$changed" | grep -E 'Assets/Tests/.*\.cs$')

if [ -n "$scripts_changed" ] && [ -z "$tests_changed" ]; then
  echo "Reminder: Assets/Scripts/*.cs changed with no matching change under Assets/Tests/. Per CLAUDE.md, gameplay logic needs a UTF test before merge. Check with 'task test' (or 'task test:editmode' / 'task test:playmode')."
fi

exit 0
