# Hive-Project

"Hive" is a dating application made on ASP.NET using Model-View-Controller (MVC) architecture.
The application mentioned above was requested as a team project, for completion of Coding Bootcamp 6, C# Stream - Part Time.

### Description:
Hive stores a user profile, fully customizable from the user's perspective, including an image as avatar and a short bio. The user will appear on the map, using geolocation, as will appear all the possible matches. For another user to be considered as a possible match, two criteria need to be fulfilled.
The first is to be near the active user, as he has specified in his desired radius. A distance measured in kilometres.
The second one, is to match the active user preferences regarding genders, meaning that should you are near the active user while your sex is not on his preference list, you will not appear on the map.
Should a profile that matches the user's criteria is found, he can choose to either Like or Dislike it. Then that profile will disappear from the user's map forever. If the profile is liked and the user get's liked back in return, an option for the two users to chat, will become available. Keep in mind, that no user can in advance know, if his/hers/its profile is liked/disliked or even viewed from another user.

### Built with:
* [Leafelt](https://leafletjs.com/) - Interactive map and markers
* [Mapbox](https://www.mapbox.com/) - For the map's tileset
* [jQuery](https://jquery.com/) - View & Controller communication
* [Bootstrap](https://getbootstrap.com/) - General styling
* [ASP.NET Identity](https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity) - User registration and authentication
* [Entity Framework](https://docs.microsoft.com/en-us/ef/) - As the object-relational mapping (ORM) framework
* [SignalR](https://dotnet.microsoft.com/apps/aspnet/real-time) - Real-time chat and messaging system

