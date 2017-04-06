# README #

Read me before you start doing the programming project.

* Branch 

## Git Related ##

### Git workflow for this project: ###

Here is a basic workflow for the project. I try to standardise everything so that you don't need to read a lot of stuffs to get this working.

1. Clone the repository to your local workstation.

        git clone https://wingkeicheung@bitbucket.org/wingkeicheung/sinexwebapp.git

2. Pick a feature to work on. Create a branch and switch to it.
      
        git check-out -b [new-branch-name] [existing-branch-name]


3. Edit, stage, and commit changes, building up the feature with as many commits as necessary.

        git add [file-name] # Stage a file
        git commit # Commit the staged files

4. Push the feature branch up to the central repo so that others can see the new branch.

        git push origin [your-branch-name]

5. Submit a pull request on the Bitbucket repo webpage.

6. After the others review the branch and accept the pull request, someone merges the feature into the master branch.

        git checkout master # Switch to master branch
        git pull # Incorporate upstream changes into local repo
        git pull origin [your-branch-name] # Merge the central repository’s copy of your upstream branch
        git push # Push back the updated master to origin.

### Useful Git Resources ###

* [Git Cheat Sheet](https://services.github.com/on-demand/downloads/github-git-cheat-sheet.pdf)

* [Git Workflows and Tutorials | Atlassian Git Tutorial](https://www.atlassian.com/git/tutorials/comparing-workflows)


## Possible Problems and Solutions ##

* If you encounter problems on building (especially on Lab's computer), try to do the following in Package Console Manager.

   ```
   uninstall-package entityframework -force
   ```

   Next, you should close your Visual Studio windows and restart your project again. Then type in your package console:

   ```
   install-package entityframework
   ```

   Build the project again and see if it works.

## Running the Tests ##

Explain how to run the automated tests for this system