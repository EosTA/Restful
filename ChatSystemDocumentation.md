# ChatSystem Server API
### Controllers
##### Messages
 * api/messages/{username} - With GET request returns paged messages between authorized user and the pointed one.
 * api/messages/{username}/{page}/{pageSize} - With GET request returns custom page sized messages between user and the pointed one.
 * api/messages - With POST request inserts message into DB. It expects json object with properties `Receiver` and `Message`.
 * api/messages/{messageId} - With PUT request modifies the message with the pointed Id. It expects json object with property `Message`. If you provide property `IsChangingDate` set to `true` (bool) it marks the message as read.
 * api/messages/all/{correspondent} - With PUT request modifies all messages between current user and the pointed one as read.
 * api/messages/{messageId} - With DELETE request deletes message with pointed Id.
 ##### Avatar
 * api/avatars - With GET request returns url of avatar as string
 * api/avatars - With POST request iterracts with Dropbox API

##### Account
* Standart WebAPI AccountController

##### User
* api/users - With GET request returns all apropriate users, provides current user to service. 
 
 
### Services
* Presence - it register users presence or online activity. When user get message, post one or do some iteraction with messages, the service updates his status in database. Default time interval for presence is set in GlobalConstants = 10 minutes.
* Messages - provides all methods required for RESTFul operations by the MessageController

# UI Documentation

Platform: `Unity3D(2d)` using ui element and event system, no outside assets imported

Scenes: LogIn, Register, Chat

* LogIn:
Error Included when wrong credentials have been included.
Buttons for login and redirection to Registration window
Two input fields one set to be a password field.
On bad request error is displayed, on good user is directed toward chat/

* Register:
Form with input fields.
Error for wrong input, which is displayed on bad request.
Buttons for seding request for registration, and going back to log in.
On succesfull registration user is redirected to Chats

* Chat:
Contacts: all available user are requested and represented as buttons to start converstation. If list is updated users are recreated.
Chat: Display two types of messages. Your messeges apear with buttons for delete and edit. After any option messages are recreated for each user, participating in the conversation currently

Request are made every 0.5 seconds to take care of updates.

All interactable objects correspoding to database objects contain the nessesary information to send the approprite request. 
