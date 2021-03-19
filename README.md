# TripCalculator

TripCalculator is a Complete CRUD app written in Asp.net Core 3.1 with ReactJs for front-end



# Install Windows Products

- Install visual studio
- install microsoft EntityFramewrorkCore, EntityFramewrorkCoreSqlServer, EntityFramewrorkCoreTools in Manage NUgetPackages

# Install Sql Server
- Install sql server and SSMS.


# Install ReactJs
- Install Node.js and NPM navigate to https://nodejs.org/en/download/ and download latest version

```bash
npm install -g npm
npx create-react-app my-app

```

#Usage

1.) Open the project up, and after installing the required libraries build the project

2.) After build, open Package Manager Console and type "update-database". Migrations are already being tracked so we just need to create the database. 

3.) Check your localhost databases and look for the TripCalcDB database and copy and paste these queries to have some students in the database
```
INSERT INTO Students Values('Louis')
INSERT INTO Students Values('Carter')
INSERT INTO Students Values('David ')
```

4.) When the project is running there will be some items in the navbar. "Trip" is where the majority of the program is located. 

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
