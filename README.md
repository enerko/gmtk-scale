# gmtk-scale
Unity Version: 2022.3.40f1 
Follow this: https://gist.github.com/j-mai/4389f587a079cb9f9f07602e4444a6ed and edit the .git/config file

to test or not to test

# What to be careful of when using git
- Pull from remote repository frequently to avoid lots of merge conflicts
- One branch per person. Never work on someone else's branch as this can cause a lot of problems
- Commit as frequently as possible for any changes you make, so that you can always go back to any point if something goes wrong (kind of like saving in game!)
- Never commit on main branch directly. Main branch should only be for merging any safe and complete (unbroken) features
- Make a Pull Request to merge your branch instead of merging your branch directly
- Branch names and commit titles are important. Keep it short, simple and informative. For branches, name them <username>-<featurename/task> e.g. enerko-player-movement, enerko-refactor, etc. For commit titles, something like "Added Pause Menu that opens with esc" and for the commit body, write more details if necessary

# Github and Unity
When merging, merge conflicts can happen often. This usually happens when two branchaes made changes on objects in the same scene. For example, if branch A moves an object from (0,0,0) to (1,0,0), and branch B moves an object from (0,0,0) to (0,1,0), then this causes a merge conflict. If we're on working with code (no Unity Editor), then resolving this merge conflict would be very simple as you only have to accept one change. However, with Unity and changes within the scene, this can be quite complicated as the merge conflict log will only show random numbers representing these changes. So we want to avoid merge conflicts cause by scene changes. To do so, there are mainly two methods.

- Work on separate branches: There is a main scene, and only one person works on this scene, while other people would duplicate the scene, make changes, merge, then manually move their changes to the main scene.
- Use Prefabs: Instead of changing objects in the scene, make changes only on the Prefabs. This allows you to manipulate objects without duplicating, then applying changes after merging. However you can't see the changes in the scene as you are working on the Prefab only.

 
