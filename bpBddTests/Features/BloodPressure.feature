Feature: Blood Pressure Category Calculation
    As a medical system
    I want to calculate a correct BP category
    So that users receive the correct health classification

    Scenario: High blood pressure based on systolic
        Given a systolic pressure of 150
        And a diastolic pressure of 70
        When I calculate the blood pressure
        Then the result should be "High Blood Pressure"

    Scenario: Ideal blood pressure
        Given a systolic pressure of 110
        And a diastolic pressure of 70
        When I calculate the blood pressure
        Then the result should be "Ideal Blood Pressure"