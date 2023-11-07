#include <Stepper.h>

// Define the pins for the first stepper motor
#define motor2Pin1 6
#define motor2Pin2 7
#define motor2Pin3 8
#define motor2Pin4 9

// Define the pins for the second stepper motor
#define motor1Pin1 10
#define motor1Pin2 11
#define motor1Pin3 12
#define motor1Pin4 13

// Define the number of steps per revolution for the stepper motors
const int stepsPerRevolution = 32;
int stepPlus = 1;
int stepMinus = -1;

Stepper stepper1(stepsPerRevolution, motor1Pin1, motor1Pin3, motor1Pin2, motor1Pin4);
Stepper stepper2(stepsPerRevolution, motor2Pin1, motor2Pin3, motor2Pin2, motor2Pin4);

void setup() {
  // Initialize the serial communication
  Serial.begin(9600);

  // Set the motor speed (RPM)
  stepper1.setSpeed(800);
  stepper2.setSpeed(800);
}

void loop() {
  if (Serial.available() > 0) {
    String command = Serial.readStringUntil('\n');
    command.trim();

    if (command == "STEPPER1_LEFT") {
      while (Serial.available() == 0) {
        stepper1.step(stepPlus); // Move the first motor left by 1 step
      }
    } else if (command == "STEPPER1_RIGHT") {
      while (Serial.available() == 0) {
        stepper1.step(stepMinus); // Move the first motor right by 1 step
      }
    } else if (command == "STEPPER2_UP") {
      while (Serial.available() == 0) {
        stepper2.step(stepPlus); // Move the second motor up by 1 step
      }
    } else if (command == "STEPPER2_DOWN") {
      while (Serial.available() == 0) {
        stepper2.step(stepMinus); // Move the second motor down by 1 step
      }
    }
  }
}
