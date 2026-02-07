# Pvp Rush. Demo Project

Hi there! I'm glad you dropped by. This is my demo project where I'm pushing my limits in Software Architecture, Networking, and Optimization in Unity.
I’m building this not just as a game, but as a playground to implement production-grade solutions for the kind of "headache" problems we face in real-world gamedev.

## How I Structured the Project

I wanted to keep things clean from the start, so I split the project into three distinct parts: **Client**, **Server**, and **Shared**.
- Shared Logic is a Package: I decided to integrate the Shared folder into Unity using the Package Manager. It’s linked via Assembly Definitions, which feels very robust.
- Keeping it Lean: I turned on the "No Engine References" toggle for the Shared assembly. I did this to make sure I don't accidentally pull in any Unity Engine/Editor code into the Server or Shared logic. It’s a great way to keep the core logic portable and safe for headless environments.
- The Bridge: Both the Client and the Server see this common code. This means they are always perfectly aligned—when I define a packet template once, both sides know exactly how to talk to each other.


## What I’m Aiming For
- Solid Architecture: I'm applying SOLID and scalable patterns so the project doesn't turn into spaghetti as it grows.
- Low-Level Networking: I’m building my own networking stack from scratch. Why? Because I want to understand every byte that goes over the wire.
- Performance: I’m planning to use multithreading (Jobs/Tasks) for the heavy lifting.

## Current Focus: The Networking Module
Right now, I’m deep in the "engine room" working on a custom UDP-based transport.
What I’ve implemented so far:
Direct Sockets: I’m using System.Net.Sockets with pure UDP. It’s faster and avoids the "Head-of-Line blocking" you get with TCP.
My Reliability Layer: I've added a lightweight ACK (Acknowledgement) system. I use it for critical stuff like "I hit that player!", while keeping things like "I'm moving to this position" unreliable to save bandwidth and time.
Zero-GC Goal: I’m doing manual bit-packing and buffer management. I want to keep the Garbage Collector quiet and the packet size as small as possible.
Multithreaded Handling: I moved the packet processing to a separate thread. This keeps the Unity Main Thread free to do what it does best: rendering frames.

---

## Tech I’m Using
- Unity 6000.3+ (LTS) (Keeping it up to date!)
- C# (.NET Standard 2.1)
- Custom UDP Transport (My own "baby")
- Multithreading: C# Job System & Tasks
