# CaesarsTest

Simple Guest REST api with authentication. CRUD operations allow for getting all guests, a specific guest, creating a guest and updating a guest. 

When a specific guest is retrieved/created/updated, that guest entity is then cached.

# FYIs

- Users for authentication are hard coded. There are 2, admin and kthomas. Admin has a role of admin which allows for reads and writes while kthomas has a role of user which only allows for reads
- Encryption is handled by using SQL server always encrypted. All PII fields on the guest entity are set as encrypted columns in SQL.