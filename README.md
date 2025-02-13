# Driving License Management System

## Overview

The Driving License Management System is a Windows desktop application designed to manage the entire process of driving license issuance, applications, tests, and driver records. The application provides a structured and user-friendly interface for government authorities, driving schools, and administrators to efficiently handle licenses, tests, and applicant data.

## Key Features

1. **User Authentication & Management**
    * Secure user login system to manage access control.
    * Role-based access to ensure only authorized users can manage license-related tasks.
    * Stores personal details of users and applicants, including nationality, contact information, and profile images.

2. **Driver & License Management**
    * Register new drivers and maintain a record of their applications and issued licenses.
    * Track license issuance, expiration, fees, and renewal processes.
    * Support for different license classes, including age restrictions and validity duration.
    * Manage detained licenses, fines, and release processes.

3. **Application & Approval Workflow**
    * Applicants can submit driving license applications through the system.
    * Administrators can review, approve, or reject applications.
    * Tracks application fees, status updates, and approval history.

4. **Driving Test Scheduling & Results**
    * Schedule and manage driving test appointments.
    * Different test types (written, practical) with associated fees.
    * Store and retrieve test results, allowing applicants to retake tests if necessary.

5. **Country & Nationality Support**
    * Manages nationality and residency details for applicants.
    * Integrated with country-based regulations for licensing.

## Technology Stack

*   **Programming Language:** C# (.NET Framework)
*   **UI Framework:** Windows Forms (WinForms)  
*   **Database:** Microsoft SQL Server
*   **Architecture:** Layered architecture for scalability and maintainability
                                                                                                
## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/DrivingLicenseManagementSystem.git
    ```
2. Open the solution file in Visual Studio.
3. Restore the NuGet packages.
4. Build the solution.

## Usage

1. Run the application from Visual Studio or the executable file.
2. Log in with your credentials.
3. Navigate through the application using the menu to manage drivers, licenses, applications, and tests.

## Contribution

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add some feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Open a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
