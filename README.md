# Xamarin.Android MVVM Login Structure using MvvmLight 

## C# project created with Xamarin Native - Android using MVVM pattern. ##

* Required Package - MvvmLight (https://www.nuget.org/packages/MvvmLight/)

# Designs
![Mockup design](https://github.com/ijeong1/MvvmLight_Xamarin.Droid_Login/blob/main/design.png)

- Activities
  - Sign In
  - Create Account
  - Account Creation Confirmation
 
 ## Sign In
 The sign in activity has two text entries and two buttons:
 - Text Entries 
    - Username & Password
 - Buttons
   - Sign In
   - Create Accont
   
 - Functions
   - Sign in button must not be enabled until username and password entries are populated
 
   - Characters in password entry must be typed securely

   - If the account doesn't exist in local storate, an error message should show up on the screen, specifying that the account doesn't exist.
   
   -  If the password doesn't match, an error message should show up on the screen specifying that the password is incorrect.
   - If the user account exists and the password matches, a success message should show up on the screen
   
## Create Account
The Create Account screen has four text entries, one text entry with DatePicker and a button.
- Text Entries
  - First Name
  - Last Name
  - Phone Number
  - Password
  - Service Start Date (UX Component with DataPicker)

- Button
  - Create Account
 
- Functions
  - The Create Account button will be enabled when all the fields are populated with values.
  - First Name and Last name should not contain these special characters (!@#$%^&)
  - Password should be 8 to 15 charaters
  - Password should have at least one lowercase letter
  - Password should have at least one uppercase letter
  - Password should not have repetitive any sequence of characters (abcabc, 111, 123a123a)
  - Phone Number shild should be formatted to (XXX)-XXX-XXXX
  - a past date is not allowd for the Service Start Date
  - a value more than 30 days into the future should throw an error
  - Once the button is tapped, the app must save the user's date in the local storage of the device
  
## Account Creation Confirmation
This activity displays a message centered verticlly and horizontally that the account was successfully created.

- Functions
   - If the user tries to go to the previos screen, they should land on the sign in activity, instead of the User Creation account.
   - If the  user taps in the Log in button, it must redirect to the Sign In activity
