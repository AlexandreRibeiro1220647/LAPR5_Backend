# US19 - To List Operation Requests

## 1. Requirements Engineering

### 1.1. User Story Description

As a Doctor, I want to list/search operation requisitions, so that I see the details,
edit, and remove operation requisitions

### 1.2. Acceptance Criteria
**AC1:** Doctors can search operation requests by patient name, operation type, priority, and status.
**AC2:** The system displays a list of operation requests in a searchable and filterable view.
**AC3:** Each entry in the list includes operation request details (e.g., patient name, operation type,
status).
**AC4:** Doctors can select an operation request to view, update, or delete it.

## 1.2.1 Acceptance Criteria From Questions To the Client

**AC5** - Doctors can search by patientId.


### 1.4. Found out Dependencies

There is a dependency to US20 and US16.

### 1.5 Input and Output Data

**Input Data:**

* Typed data:
    * patient name
    * patientId
    * operation type
    * priority
    * deadline

**Output Data:**

* Filtered requests list
* (In)Success of the operation
