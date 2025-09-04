# USA Elections  

A web application for managing and visualizing data related to U.S. elections. This is part of firm task.
Built with **ASP.NET Core MVC** and a clean separation of concerns using services for database and file operations.  

## Demo  

🎥 [Watch the demo here](https://drive.google.com/file/d/157bYBqXzowj389MGM1-Q9IWqEoEhv0Jt/view?usp=sharing)  


## Tech Stack  

- **ASP.NET Core MVC** – Web framework for the application  
- **Services Layer** – Handles all database and file interactions, ensuring a clean architecture  

## Database Design  

The database currently consists of the following tables:  

- **Candidate** – Stores information about each election candidate  
- **Constituency** – Contains data about constituencies  
- **Vote** – Keeps track of votes  

This modular design makes it easy to add new records or expand the schema as needed.  

📄 [View the ERD](https://drive.google.com/file/d/15bHqeY7DvINK0Nr9jJ_lp4cPHZqxkLuC/view?usp=sharing)  

## Validation  

Incorrect entries are automatically separated into a dedicated error file.  
This ensures that invalid records do not get inserted into the database and makes error handling more transparent.  

## Features  

- CRUD operations for election data  
- Clean architecture with services  
- Validation of input data with error logging  
- Scalable database design  

## Future Improvements  

- Adding richer candidate details (e.g. biography, political history)  
- Incorporating additional geographic data (population, demographics)  
- More advanced reporting and visualization tools  

---
