Feature: Blood Pressure Category Calculation
    As a medical system
    I want to calculate a correct BP category
    So that users receive the correct health classification

    # HIGH BLOOD PRESSURE
    Scenario: High blood pressure based on systolic
        Given a systolic pressure of 150
        And a diastolic pressure of 70
        When I calculate the blood pressure
        Then the result should be "High Blood Pressure"
    
    Scenario: High blood pressure by diastolic
        Given a systolic pressure of 120
        And a diastolic pressure of 95
        When I calculate the blood pressure
        Then the result should be "High"

    # PRE-HIGH BLOOD PRESSURE 
    Scenario: Pre-high blood pressure by systolic
        Given a systolic pressure of 130
        And a diastolic pressure of 75
        When I calculate the blood pressure
        Then the result should be "PreHigh"

    Scenario: Pre-high blood pressure by diastolic
        Given a systolic pressure of 118
        And a diastolic pressure of 85
        When I calculate the blood pressure
        Then the result should be "PreHigh"

    # IDEAL BLOOD PRESSURE 
    Scenario: Ideal blood pressure
        Given a systolic pressure of 110
        And a diastolic pressure of 70
        When I calculate the blood pressure
        Then the result should be "Ideal"

    Scenario: Ideal at lowest diastolic boundary
        Given a systolic pressure of 100
        And a diastolic pressure of 40
        When I calculate the blood pressure
        Then the result should be "Ideal"

    # LOW BLOOD PRESSURE 
  Scenario: Low blood pressure by systolic
    Given a systolic pressure of 85
    And a diastolic pressure of 55
    When I calculate the blood pressure
    Then the result should be "Low"

  Scenario: Low blood pressure by diastolic
    Given a systolic pressure of 100
    And a diastolic pressure of 35
    When I calculate the blood pressure
    Then the result should be "Low"