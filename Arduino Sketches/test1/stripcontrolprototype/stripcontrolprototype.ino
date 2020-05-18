#define FASTLED_ESP8266_RAW_PIN_ORDER

#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#include <FastLED.h>

#define LED_DATA_SIZE 5
#define _KEY_ XXX

#define NUM_LEDS 300
#define DATA_PIN 3 //gpio3

const char* _SSID = "XXX";
const char* PASSWORD = "XXX";

const long TIMEOUT = 2000;

//const byte LED_UPDATE_PACKET_SIZE = 5;

CRGB leds[NUM_LEDS];

WiFiUDP udp;
char newPacket[UDP_TX_PACKET_MAX_SIZE];
char replyPacket[] = "GOOD";
const unsigned int PORT = XXX;

bool DEBUG = false;

void setup() {
  Serial.begin(115200);
  Serial.println();

  FastLED.addLeds<WS2812B, DATA_PIN, GRB>(leds, NUM_LEDS);

  StartWiFi();

  udp.begin(PORT);
}

void loop() {
  if(receivedPacket())
    if(parseContents()) respond();
    else Serial.println("Invalid key received.");
}

String IPAddressToString(IPAddress address) {
  String result;
  for(int i = 0; i < 4; ++ i){
    result+=address[i];
    if(i < 3) result+=".";   
  }
  return result;
}

String dumpPacketContent(char packet[], int len) {
  String result = "";
  for(int i = 0; i < len; ++ i)
  {
    result += "["; result += packet[i]; result += "]";
  }
  return result;
}

void StartWiFi() {
  Serial.print("Connecting to ");
  Serial.println(_SSID);
  WiFi.begin(_SSID, PASSWORD);
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println();
  Serial.println("WiFi connected.");
  Serial.print("IP: ");
  Serial.println(WiFi.localIP());
}

int packetSize = 0;

bool receivedPacket() {
  packetSize = udp.parsePacket(); //checks for packet and records length
  if(packetSize) { //if length != 0
    if(DEBUG) {
      Serial.print("Received packet with size: ");
      Serial.println(packetSize);
      Serial.print("From: ");
      String ip = IPAddressToString(udp.remoteIP());
      Serial.print(ip+":");
      Serial.println(udp.remotePort());
    }
    udp.read(newPacket, UDP_TX_PACKET_MAX_SIZE);
    return true;
  }
  return false;
}

bool parseContents() {
  if(!isSafePacket())
    return false;
        
  //Serial.print("Contents: ");
  //Serial.println(dumpPacketContent(newPacket, packetSize));
  //Serial.println("TEST");
  int ledUpdates = (packetSize - 1) / LED_DATA_SIZE;
  for(int i = 0; i < ledUpdates; ++ i) {
    int pad = i * LED_DATA_SIZE;
    int led_index = (int) newPacket[pad+1] * 256 + (int) newPacket[pad+2];
    int red = (int) newPacket[pad+3];
    int green = (int) newPacket[pad+4];
    int blue = (int) newPacket[pad+5];

    leds[led_index] = CRGB(red, green, blue);
    if(DEBUG) {
      Serial.print(led_index);
      Serial.print(", ");
      Serial.print(red);
      Serial.print(", ");
      Serial.print(green);
      Serial.print(", ");
      Serial.println(blue);
    }
  }

  FastLED.show();
  
  return true;
}

void respond() {
  udp.beginPacket(udp.remoteIP(), udp.remotePort());
  udp.write(replyPacket);
  udp.endPacket();
}

bool isSafePacket() {
  return newPacket[0] == _KEY_ && (packetSize - 1) % LED_DATA_SIZE == 0;
}
