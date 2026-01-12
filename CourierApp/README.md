Courier Service Console Application

A simple console-based courier service application built in C# that calculates delivery cost, applies discount offers, assigns vehicles, and estimates delivery time.

Features

	Enter base delivery cost
	Add multiple packages
	Apply discount offers based on rules
	Add custom offers dynamically
	Validate user input
	Calculate final delivery cost
	Calculate delivery time
	Restart the application
	Application Flow

Setup
	-> Show Vehicles
	-> Show Offers
	-> Add New Offer (Optional)
	-> Add Packages
	-> Calculate Cost and Time
	-> Show Summary
	-> Restart or Exit

Technologies Used

C#
.NET 8 : Console Application
Object-Oriented Programming (OOP)

How to Run the Application

Clone the repository:
git clone https://github.com/9167136086/CourierApp

Open the solution in Visual Studio or any C# IDE

Build and run the project:
dotnet run

Step-by-Step Usage Guide

		Step 1: Courier Service Setup

			You will be asked to enter:

			Enter Base Delivery Amount : 100
			Enter Number of Packages : 2

			Only numeric input is allowed

			Invalid input will be rejected and asked again

		Step 2: Available Vehicle Details

			The system automatically displays available vehicles:

			Vehicle Number: 1, Max Weight: 200, Speed: 70
			Vehicle Number: 2, Max Weight: 200, Speed: 70

			No user input is required in this step.

		Step 3: Available Offers

			Default discount offers are shown:

			Code: OFR001, Discount: 10%, Weight Range: 70-200, Max Distance: 200
			Code: OFR002, Discount: 7%, Weight Range: 50-150, Max Distance: 200
			Code: OFR003, Discount: 5%, Weight Range: 50-250, Max Distance: 200

		Step 4: Add New Offer (Optional)

			You will be asked:

			Do you want to add a new offer? (Y/N):

			If you choose Y, enter the following details:

			Offer Code

			Discount Percentage

			Minimum Weight

			Maximum Weight

			Maximum Distance

			Validation Rules:

			Minimum Weight must be less than Maximum Weight

			Only numeric values are allowed where required

			Invalid input will be rejected

		Step 5: Add Packages

			For each package:

			Add a new Package? (Y/N):
			Weight:
			Distance:
			Available Offer Codes: OFR001, OFR002, OFR003
			Offer Code:

			Select an offer code from the displayed list

			If offer conditions are not met, discount will be 0

			After successful input:
			Package added successfully!

		Step 6: Package Summary Output

			After all packages are added, the application displays a summary:

			PKG_ID WEIGHT DIST OFFER TOTAL DISC FINAL TIME

			Column Description:
			PKG_ID - Package ID
			WEIGHT - Package weight
			DIST - Delivery distance
			OFFER - Applied offer code
			TOTAL - Total delivery cost
			DISC - Discount amount
			FINAL - Final payable amount
			TIME - Delivery time in hours

		Step 7: Restart or Exit

			At the end, you will see:

			Do you want to start again? (Y/N):

			Enter Y to restart the application

			Enter N to exit the application

			Important Input Rules

			Enter numbers only in numeric fields

			Choose offer codes only from the displayed list

			Do not enter text in numeric fields

			Minimum Weight must always be less than Maximum Weight



Author

Developed by Ramakant Shinde
