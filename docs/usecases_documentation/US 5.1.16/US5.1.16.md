# US16 - To Create an OperationRequest


## 1. Requirements Engineering

### 1.1. User Story Description

As a Doctor, I want to request an operation, so that the Patient has access to the necessary healthcare.

### 1.2. Acceptance Criteria

**AC1** - Doctors can create an operation request by selecting the patient, operation type, priority, and
suggested deadline.

**AC2** - The system validates that the operation type matches the doctor’s specialization.
**AC3** - The operation request includes:
- Patient ID
- Doctor ID
- Operation Type
- Deadline
- Priority

**AC4** - The system confirms successful submission of the operation request and logs the request in
the patient’s medical history.

## 1.2.1 Acceptance Criteria From Questions To the Client

**AC5** - Not possible to have more than one operationRequest for the same patient and operationType
    

### 1.3. Found out Dependencies

* There is a dependency to 5.1.8 and 5.1.12 because we need a patient and a doctor to do a operationRequest.

### 1.4 Input and Output Data

**Input Data:**

* Typed data:
    * patientId
    * doctorId
    * operationType
    * priority
    * deadline
    * 
**Output Data:**

* (In)Success of the operation