<h1 align="center">
  Vasilis Plavos - React Native Web Test Task - README
</h1>

## Overview
This project implements a print functionality for a React Native Web application. The goal is to generate a production-grade PDF document that accurately reflects the web layout without any additional margins, headers, or unwanted artifacts.

## 🚀 Quick start - Prepare Application on VS Code
1. Go to print-app directory
1. Run `npm install` to load the package.json dependencies
1. Run `npm run web` to start the project

## Features
1. Client-side printing using window.print()
1. Server-side printing using Azure Function
    1. Deployed on Azure Cloud using ACR and Dockerfile
    1. Used Playwright to access the Html Elements at the server-side
    1. Upload of the PDF files at the Azure Storage Account for faster response and resilience

## Folder Structure
A quick look at the projects:

    ├── print-app
    ├──── App.js
    ├── azf-nodejs-playwright-docker
    ├──── Dockerfile
    ├──── src > functions
    ├──────── print.ts

1.  **`/print-app`**: This directory contains the React Native application
1.  **`/App.js`**: This file contains the requested implementation of the application
1.  **`/azf-nodejs-playwright-docker`**: This directory contains the project of the Azure Function
1.  **`/Dockerfile`**: This file is the Dockerfile deployed at the ACR
1.  **`print.ts`**: This file is the implementation of the Azure Function

## Implementation Details

#### 1. Client-Side approach and Decision-making

Since the dependencies are fixed and I was not able to import other packages, in order to achieve as identical as possible results, I am creating a new HTML Document.
In that HTML Document, I am injecting the same style rules and the div elements of the two pages.

#### 2. Server-Side approach and Decision-making

The server is responsible for receiving the HTTP request, generating the PDF, uploading it to the Blob Storage, and returning a URL of the generated PDF file to the client.

##### Features:
- Use of Dockerfile to set up the environment of the application.
- Receiving requests to process and generate PDFs.
- Using a headless browser (Playwright) to capture PDF snapshots.
- Uploading the file to Azure Blob Storage.
- Using SAS Token to deny access to the file after 30 minutes.
- Lifecycle management to delete files after 3 days automatically.

##### Benefits of the server-side approach:
- Protect the results: Avoidance of client-side data manipulation. In the financial industry, often, it is important to ensure that printed documents are not manipulated. With server-side printing, the risk is mitigated since the client cannot easily alter the data.
- Flexibility in the cross-platform approach: The server-side approach minimizes inconsistencies in printed documents across different platforms since the process runs in the same controlled environment every time.

#### 3. Dockerfile Explanation
The Dockerfile enables containerized deployment, ensuring the project runs consistently across environments. It:
* Sets up an Azure Function environment with Node.js.
* Installs dependencies.
* Uses ACR to publish the image.