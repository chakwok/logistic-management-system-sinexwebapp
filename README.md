# README #

Read me before you start doing the programming project.

## Getting Started ##

### Git workflow for this project: ###

Here is a basic workflow for the project. I try to standardise everything so that you don't need to read a lot of stuffs to get this working.

1. Clone the repository to your local workstation.

        git clone https://wingkeicheung@bitbucket.org/wingkeicheung/sinexwebapp.git

2. Pick a feature to work on. Create a branch and switch to it.
      
        git checkout -b [new-branch-name] [existing-branch-name]


3. Edit, stage, and commit changes, building up the feature with as many commits as necessary.

        git add . # Stage new files
        git commit -m "[descriptive message]" # Commit the staged files

4. Push the feature branch up to the central repo so that others can see the new branch.

        git push origin [your-branch-name]

5. Submit a pull request on the Bitbucket repo webpage.

6. ============= Do not proceed to this step until your pull request has been approved. =============

    After the others review the branch and accept the pull request, someone merges the feature into the master branch.

        git checkout master # Switch to master branch
        git pull # Incorporate upstream changes into current local branch
        git pull origin [your-branch-name] # Merge the central repository’s copy of your upstream branch into current local branch
        git push # Push back the updated master to origin.

7. Update the README.md History

### Useful Git Resources ###

* [Git Cheat Sheet](https://services.github.com/on-demand/downloads/github-git-cheat-sheet.pdf)

* [Git Workflows and Tutorials | Atlassian Git Tutorial](https://www.atlassian.com/git/tutorials/comparing-workflows)


## Q&A ##

* If you encounter problems on building the project (especially on Lab's computer), try to do the following in Package Console Manager.

        uninstall-package entityframework -force
   Next, you should close your Visual Studio windows and restart your project again. Then type in your package console:
   
        install-package entityframework
   Build the project again and see if it works.

* If you want to roll back migrations and initialise the database, you may use the following commands.

        update-database -targetmigration:0 -configurationtypename AspIdentityConfiguration -force
        update-database -targetmigration:0 -configurationtypename SinExConfiguration -force
        update-database -configurationtypename AspIdentityConfiguration
        update-database -configurationtypename SinExConfiguration

## History ##

*README.md Revision History*

   | Time               | Author           | Comment                        |
   | ------------------ |:----------------:| ------------------------------:|
   | 2017/04/06 22:00   | Cheung Wing Kei  | Initial Commit.                |  
   | 2017/04/10 02:18   | Cheung Wing Kei  | Updated Q&A.                   |  
   | 2017/04/11 17:30   | Cheung Wing Kei  | Updated Git workflow Step 6.   | 
   | 2017/04/17 16:33   | Cheung Wing Kei  | Updated Git workflow Step 4.   |  


*Repository Master Branch Revision History*

   | Time               | Author           | Comment                                          |
   | ------------------ |:----------------:| ------------------------------------------------:|
   | 2017/04/04 23:54   | Cheung Wing Kei  | Packages Merge Successful.                       |
   | 2017/04/10 02:30   | Cheung Wing Kei  | ShippingAccountId successfully changed to int.   |
   | 2017/04/04 03:30   | Cheung Wing Kei  | Edit account successful.                         |