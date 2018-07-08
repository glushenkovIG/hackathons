NuCypher Hackathon

Hello everybody, my name is Ivan and I represent the team <Seroronin syndrome> that has been working on the NuCypher company’s case.
We studied the proxy re-encryption protocol and the software given by NuCypher (mockNet and Python pyUmbral library).
Using this protocol and the mockNet system we have developed the messenger with the perfect level of security and private data protection.

Message sending process from Alice to Bob.

Encypted with Alice’ private key, the message is sent to Ursula (a third-party entity). Ursula creates a capsula and puts the message into it. Then, Ursula encrypts the capsula once again, this time with Bob’s public key. Finally, Bob sends a request to Ursulla to open the capsula using his private key. This request is approved if and only if Bob’s private key matches the public key of the recipient of the message.

Under the hood we have an old-school command-line messenger, serving as a proof-of-concept of sending and accessing messages. Note that instead of using the usual client-server model, it uses our distributed protocol and the infrastructure provided by NuCypher.
