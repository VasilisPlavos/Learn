# git-overwrite-branch (overwrite master with contents of feature branch (feature > master))
git checkout feature    # source name
git merge -s ours master  # target name
git checkout master       # target name
git merge feature       # source name