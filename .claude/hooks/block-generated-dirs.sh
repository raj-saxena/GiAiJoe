#!/bin/bash
# PreToolUse guard: deny Read/Grep/Glob into large/generated Unity dirs.
# These are gitignored but still readable, and expensive to pull into context.
input=$(cat)
denylist='(^|/)(Library|Temp|Build|obj|Logs)(/|$)'

values=$(echo "$input" | jq -r '.tool_input | .. | strings' 2>/dev/null)
if echo "$values" | grep -Eq "$denylist"; then
  echo "Blocked: target is under a generated/bulky Unity directory (Library/, Temp/, Build/, obj/, or Logs/). These are gitignored and not meant to be read into context. If you specifically need something from here, ask the user directly." >&2
  exit 2
fi

exit 0
