Task 1 - Completed Task
-------------------------

1. Employed a 3-tier architecture to facilitate readability, comprehension, and future modifications in the email sending project.
2. Made adjustments to the SMTP server settings for email transmission.
3. Stored all configuration values and email templates in the appsettings.json file, allowing for easy content modifications, such as utilizing Azure app config values.
4. Substituted hard-coded text with global constant variables and application configuration values, enabling effortless value alterations at any given time.
5. Developed a singular method capable of handling the sending of various email types.
6. Implemented exception handling as and when required.
7. Utilized LINQ to filter the list of users.
8. Ensured that sensitive information, such as the email sender's password, remains unlogged.
9. Employed appropriate and relevant naming conventions for classes, variables, and methods.
10. Tested the email functionality and confirmed its successful operation using my gmail credentials.


Task 2 - Theoretical task
--------------------------

We have the option to utilize Microsoft Azure tools like Azure function app and Azure Queue technologies. By doing so, we are able to add an entry into the azure queue whenever there is a need to send an email.
Once the message is added to the queue, the azure function will handle the request and send the email to the appropriate recipient. This approach allows for asynchronous email sending, resulting in a more responsive console application.
Additionally, we can incorporate logging features into the application. This will assist us in quickly identifying the underlying issue when errors occur.
