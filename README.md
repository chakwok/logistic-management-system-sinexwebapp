# README #

Read me before you start doing the programming project.

### What is this repository for? ###

* Quick summary
* Version

### How do I get set up? ###

* Summary of set up

    Git workflow for this project:

	1. Clone the repository to your local workstation.

		```
		git clone https://wingkeicheung@bitbucket.org/wingkeicheung/sinexwebapp.git
		```

	2. Pick a feature to work on. Create a branch and switch to it.
	   
		```
		git check-out -b [branch-name]
		```

	3. Make lots of commits until you have something ready to send to the repo.

         ```
        git add [file-name] # Stage a file
        git commit # Record what has been staged so far
        ```

	4. To update your local repository to the newest commit.

		```
		git pull --rebase origin master
		```

	5. After synchronising with the central repo, 
	 
		```
		git push origin master # Publish changes to the central repository
		```



* Configuration

	1. If you encounter problems on building (especially on Lab's computer), try to do the following in Package Console Manager.

		```
		uninstall-package entityframework -force
		```

		Then you should close your Visual Studio windows and restart your project again. Type in your package console:

		```
		install-package entityframework
		```
		
		Build the project again and see if it works.

* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact