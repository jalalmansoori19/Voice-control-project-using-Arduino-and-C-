/* This Program is about controlling leds states ON and OFF using voice recognition library of c# (using system.speech) 
 * Exciting Part is you dont need to have any external module to transmit data to arduino because you can easily  
 * use your builtin computer microphone or earphones microphone.   
 *  
 * This Program is just to give basic idea specially to beginners and then its your own creativity how you use it in a useful way.
 * Keep Learning, Share, think and Repeat
 * Enjoy !
 * 
 * By Jalal Mansoori
 */

const int blueLed=10;
const int redLed=9;
const int greenLed=8;

char incomingData='0';
 
void setup() {
  // put your setup code here, to run once:
//getting leds ready
Serial.begin(9600);
pinMode(blueLed, OUTPUT);
pinMode(redLed, OUTPUT);
pinMode(greenLed, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  incomingData=Serial.read();

  // Switch case for controlling led in our case we have only 3 Blue, Green and Red 
  switch(incomingData)
  {
      //These cases are only for state ON of led
      // For blue led
      case 'B':
      digitalWrite(blueLed, HIGH);
      break;    
      
      // For red led
      case 'R':
      digitalWrite(redLed, HIGH);
      break;    
      
      // For green led
      case 'G':
      digitalWrite(greenLed, HIGH);
      break;    

      //These cases are  for state OFF of led and case name z , x, c are just randomly given you can also change but 
      // make sure you change it in a c# code as well.
      // For blue led
      case 'Z':
      digitalWrite(blueLed, LOW);
      break;    
      
      // For red led
      case 'X':
       digitalWrite(redLed, LOW);
      break;    
      
      // For green led
      case 'C':
       digitalWrite(greenLed, LOW);
      break;

      //For turning ON all leds at once :)
      case 'V':
      digitalWrite(blueLed, HIGH);
      digitalWrite(redLed, HIGH);
      digitalWrite(greenLed, HIGH);
      break;

      //For turning OFF all leds at once :)
      case 'M':
      digitalWrite(blueLed, LOW);
      digitalWrite(redLed, LOW);
      digitalWrite(greenLed, LOW);
      break;
      
  }
}
