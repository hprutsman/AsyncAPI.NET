using LEGO.AsyncAPI;
using LEGO.AsyncAPI.Models;
using LEGO.AsyncAPI.Readers;

var jsonDoc = """
{
  "asyncapi": "2.6.0",
  "info": {
    "title": "Streetlights Kafka API",
    "version": "1.0.0",
    "description": "The Smartylighting Streetlights API allows you to remotely manage the city lights.\n\n### Check out its awesome features:\n\n* Turn a specific streetlight on/off 🌃\n* Dim a specific streetlight 😎\n* Receive real-time information about environmental lighting conditions 📈\n",
    "license": {
      "name": "Apache 2.0",
      "url": "https://www.apache.org/licenses/LICENSE-2.0"
    }
  },
  "servers": {
    "scram-connections": {
      "url": "test.mykafkacluster.org:18092",
      "protocol": "kafka-secure",
      "description": "Test broker secured with scramSha256",
      "security": [
        {
          "saslScram": [ ]
        }
      ],
      "tags": [
        {
          "name": "env:test-scram",
          "description": "This environment is meant for running internal tests through scramSha256"
        },
        {
          "name": "kind:remote",
          "description": "This server is a remote server. Not exposed by the application"
        },
        {
          "name": "visibility:private",
          "description": "This resource is private and only available to certain users"
        }
      ]
    },
    "mtls-connections": {
      "url": "test.mykafkacluster.org:28092",
      "protocol": "kafka-secure",
      "description": "Test broker secured with X509",
      "security": [
        {
          "certs": [ ]
        }
      ],
      "tags": [
        {
          "name": "env:test-mtls",
          "description": "This environment is meant for running internal tests through mtls"
        },
        {
          "name": "kind:remote",
          "description": "This server is a remote server. Not exposed by the application"
        },
        {
          "name": "visibility:private",
          "description": "This resource is private and only available to certain users"
        }
      ]
    }
  },
  "defaultContentType": "application/json",
  "channels": {
    "smartylighting.streetlights.1.0.event.{streetlightId}.lighting.measured": {
      "description": "The topic on which measured values may be produced and consumed.",
      "publish": {
        "operationId": "receiveLightMeasurement",
        "summary": "Inform about environmental lighting conditions of a particular streetlight.",
        "traits": [
          {
            "$ref": "#/components/operationTraits/kafka"
          }
        ],
        "message": {
          "$ref": "#/components/messages/lightMeasured"
        }
      },
      "parameters": {
        "streetlightId": {
          "$ref": "#/components/parameters/streetlightId"
        }
      }
    },
    "smartylighting.streetlights.1.0.action.{streetlightId}.turn.on": {
      "subscribe": {
        "operationId": "turnOn",
        "traits": [
          {
            "$ref": "#/components/operationTraits/kafka"
          }
        ],
        "message": {
          "$ref": "#/components/messages/turnOnOff"
        }
      },
      "parameters": {
        "streetlightId": {
          "$ref": "#/components/parameters/streetlightId"
        }
      }
    },
    "smartylighting.streetlights.1.0.action.{streetlightId}.turn.off": {
      "subscribe": {
        "operationId": "turnOff",
        "traits": [
          {
            "$ref": "#/components/operationTraits/kafka"
          }
        ],
        "message": {
          "$ref": "#/components/messages/turnOnOff"
        }
      },
      "parameters": {
        "streetlightId": {
          "$ref": "#/components/parameters/streetlightId"
        }
      }
    },
    "smartylighting.streetlights.1.0.action.{streetlightId}.dim": {
      "subscribe": {
        "operationId": "dimLight",
        "traits": [
          {
            "$ref": "#/components/operationTraits/kafka"
          }
        ],
        "message": {
          "$ref": "#/components/messages/dimLight"
        }
      },
      "parameters": {
        "streetlightId": {
          "$ref": "#/components/parameters/streetlightId"
        }
      }
    }
  },
  "components": {
    "schemas": {
      "lightMeasuredPayload": {
        "type": "object",
        "properties": {
          "lumens": {
            "type": "integer",
            "description": "Light intensity measured in lumens.",
            "minimum": 0
          },
          "sentAt": {
            "$ref": "#/components/schemas/sentAt"
          }
        }
      },
      "turnOnOffPayload": {
        "type": "object",
        "properties": {
          "command": {
            "type": "string",
            "description": "Whether to turn on or off the light.",
            "enum": [
              "on",
              "off"
            ]
          },
          "sentAt": {
            "$ref": "#/components/schemas/sentAt"
          }
        }
      },
      "dimLightPayload": {
        "type": "object",
        "properties": {
          "percentage": {
            "type": "integer",
            "description": "Percentage to which the light should be dimmed to.",
            "maximum": 100,
            "minimum": 0
          },
          "sentAt": {
            "$ref": "#/components/schemas/sentAt"
          }
        }
      },
      "sentAt": {
        "type": "string",
        "format": "date-time",
        "description": "Date and time when the message was sent."
      }
    },
    "messages": {
      "lightMeasured": {
        "payload": {
          "$ref": "#/components/schemas/lightMeasuredPayload"
        },
        "contentType": "application/json",
        "name": "lightMeasured",
        "title": "Light measured",
        "summary": "Inform about environmental lighting conditions of a particular streetlight.",
        "traits": [
          {
            "$ref": "#/components/messageTraits/commonHeaders"
          }
        ]
      },
      "turnOnOff": {
        "payload": {
          "$ref": "#/components/schemas/turnOnOffPayload"
        },
        "name": "turnOnOff",
        "title": "Turn on/off",
        "summary": "Command a particular streetlight to turn the lights on or off.",
        "traits": [
          {
            "$ref": "#/components/messageTraits/commonHeaders"
          }
        ]
      },
      "dimLight": {
        "payload": {
          "$ref": "#/components/schemas/dimLightPayload"
        },
        "name": "dimLight",
        "title": "Dim light",
        "summary": "Command a particular streetlight to dim the lights.",
        "traits": [
          {
            "$ref": "#/components/messageTraits/commonHeaders"
          }
        ]
      }
    },
    "securitySchemes": {
      "saslScram": {
        "type": "scramSha256",
        "description": "Provide your username and password for SASL/SCRAM authentication"
      },
      "certs": {
        "type": "X509",
        "description": "Download the certificate files from service provider"
      }
    },
    "parameters": {
      "streetlightId": {
        "description": "The ID of the streetlight.",
        "schema": {
          "type": "string"
        }
      }
    },
    "operationTraits": {
      "kafka": {
        "bindings": {
          "kafka": {
            "clientId": {
              "type": "string",
              "enum": [
                "my-app-id"
              ]
            }
          }
        }
      }
    },
    "messageTraits": {
      "commonHeaders": {
        "headers": {
          "type": "object",
          "properties": {
            "my-app-header": {
              "type": "integer",
              "maximum": 100,
              "minimum": 0
            }
          }
        }
      }
    }
  }
}
""";
var reader = new AsyncApiStringReader();
var doc = reader.Read(jsonDoc, out _);
var ymlDoc = doc.SerializeAsYaml(AsyncApiVersion.AsyncApi2_0);
await File.WriteAllTextAsync(@"./streetlights-kafka.yml", ymlDoc);