Skype_Extractor
===============

A small windows application that extracts skype chat history. 
For a given skype username, you are able to extract the desired 
chat history of certain skype chat contacts.

===========
Assumptions
===========

- For a given user, they have used Skype on the computer
- The user has not relocated their main.db file, which contains
  all of their chat history information

===========
Tech Notes
===========

I used Visual Studio 2013 to develop this application. I also 
ended up using two packages; EntityFrameWork(6.1.1) and the 
SQLite adapter from ADO.NET.

Current things to work out:
 - Restructuring code to become more event-driven and readable
 - Designing a better UI
    - Includes providing better feedback
