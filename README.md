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

============
Things to do
============
- Write a class that formats the chat history.
 - This includes formatting links and symbols to plaintext (regex?)
 - Indenting long messages (word wrap)
- Fix the logic to display contacts that have actual chat history and not necessarily all contacts added by the skype user
