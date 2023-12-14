Step 1
	Load the backup
		Open Sql Server management studio. Right click on databases. Import the backup (MoviesAPI.bak)
Step 2
	Due to big size I deleted node_modules folder. So type
		npm install on cmd
	Run backend 
		Go to MoviesAPI folder. Open the solution with visual studio and click the play button (start with debuging) (there is a basic swagger documentation) 

Step 3
	Run frontend
		Open angular-movies folder with visual studio code. Run the app with ng serve command

App features
	This app is a movie management system. On home page you can see some movies which are already played on cinemas (theaters) and feature releases. 

	You can click a movie (on picture) to see some basic movie information

	You can also edit and delete it from home
	
	Some basic entities are	
		Genre
			Where you can crud a movie genre (inputs: title)

		Actor
			Where you can crud an actor (inputs: Fullname, DOB, picture, Description)

		Movie theaters
			Where you can crud a cinema (inputs: name, location through a map)

	The main entity is movie so when you create a movie you can add
		Title (simple text input)
		Trailer (youtube link for movie trailer)
		Summary (simple text input)
		In theaters (checkbox if you want it to be categorized as "future release" leave it empty)
		Release date (select from date picker)
		Genres: you can add multiple (existing) genres here for one single movie
		Movie theaters: you can add here the (existing) cinemas you want the movie to be played
		Actors:  you can add multiple (existing) actors here for one single movie and their corresponding roles as well


	A personal brief evaluation
		Due to technical problems (laptop damage),there are some bugs which I couldn't resolve them from the secondary laptop
		 i.e
			1) When I edit an entity (i.e actor) that has picture null value goes on database
			2) The map of the movie info is not correct

		Finally I wanted to do a proper seperation of concerns (services, repositories). The code now is (almost) all on controllers
	 

Credits
	I want to say thanks to all instructors of coding factory. It was a great experience. I am looking forward to meet you all.

		

	