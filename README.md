# Research Study Documentation

## 6.1 Description of the Artefact

The artefact is an interface that connects multiple wireless devices to a serious game developed in the Unity Game Engine. The purpose of the serious game is to supplement bilateral integration rehabilitation activities.

**Figure 6-1: Client and Server Interfaces**

The interface was developed in Unity Game Engine to connect multiple external wireless devices to a serious game. The server (serious game instance) allows clients to connect to the game by clicking the server button, as displayed in Figure 6-1 on the left-hand side. An event is invoked to fetch the local system IPv4 address to allow the client to connect via LAN. This, however, requires the client to be connected to the same Wi-Fi network.

The client interface was also developed in Unity Game Engine, enabling external clients (mobile devices) to connect to the server (serious game instance). As seen in Figure 6-1 on the right-hand side, a client needs to enter the server IP address to connect via LAN.

The connection between the server and clients is established through the Unity Netcode for GameObject library, enabling multiplayer functionality. Unity’s server RPC is used to allow clients to invoke server methods, provided the server permits it. This setup enables clients to control the player’s movement with external mobile devices. The game requires three devices, each dedicated to a specific movement: left, right, or action.

**Figure 6-2: Gameplay**

**Figure 6-3: Gameplay Shop**

**Figure 6-4: Gameplay Data Analysis**

The serious game (Figure 6-2) supplements traditional therapy methods, encouraging activities like reading or bilateral movement exercises. The player and dog sprites move across the screen, encouraging the player to track these sprites with their eyes, thus mimicking left-to-right reading. Additionally, the wireless device can be moved horizontally or vertically, promoting free hand movement.

Engaging features in the game include:
1. **Shop (Figure 6-3)** - Unlock new dog characters, encouraging repeated gameplay sessions.
2. **AI model** - Encourages players to move further distances in the game.
3. **Data analysis (Figure 6-4)** - Helps occupational therapists assess therapy progress in each game session.

## 6.2 Life Cycle and Phases Followed

The research followed Peffers et al. (2007) process model for designing, developing, evaluating, and documenting the research artefact. The model includes the following steps:

1. **Problem Identification and Motivation**: Define the research issue, highlighting its importance (Peffers et al., 2007).
2. **Define Objectives for a Solution**: Establish objectives based on the problem, considering existing solutions (Peffers et al., 2007).
3. **Design and Development**: Create an artefact based on defined objectives (Peffers et al., 2007).
4. **Demonstration**: Apply the artefact to specific problem instances (Peffers et al., 2007).
5. **Evaluation**: Measure effectiveness through appropriate metrics (Peffers et al., 2007).
6. **Communication**: Share findings with relevant audiences (Peffers et al., 2007).

Peffer’s model allows for iterative development, starting from any entry point and revisiting steps as needed.

## 6.3 Description of the Development Process

The implementation of each process step is outlined in Table 6-1.

| Process Iteration                   | Implementation |
|-------------------------------------|----------------|
| **Problem Identification and Motivation** | Identifying challenges in bilateral integration and limitations of traditional therapy methods. |
| **Define Objectives for a Solution** | Design a serious game supporting bilateral integration and rehabilitation. |
| **Design and Development** | Develop the game in Unity, integrating visuals, audio, and wireless device compatibility. |
| **Demonstration** | Present to occupational therapy experts for feedback. |
| **Evaluation** | Collect and analyze feedback from experts. |
| **Communication** | Document and share findings through academic channels. |

## 6.4 Research Methodology

The study employed Design Science Research Methodology (DSRM) within a pragmatic paradigm, ensuring rigorous artefact development. The research followed the research onion model (Saunders, 2009), which includes:

- **Philosophy**: Pragmatism
- **Approach**: Inductive
- **Strategy**: Design science research
- **Data Collection**: Mixed methods
- **Time Horizon**: Longitudinal
- **Techniques and Procedures**: Data collection and analysis

### 6.4.1 Data Sampling, Collection, and Analysis

The survey guidelines for software engineering (Linåker et al., 2015) were followed:

- **Define objectives**: Established in Chapter One.
- **Identify audience**: Experts in serious games, occupational therapy, and relevant educators.
- **Sampling plan**: Feedback focused on experts.
- **Design survey instrument**: Developed an open/closed-ended questionnaire.
- **Instrument evaluation**: Feedback collected from research leaders.
- **Data analysis**: Sentiment analysis and quantitative scoring (1-5).
- **Conclude and document**: Findings compiled in a mini dissertation.

### 6.4.2 Results

#### 6.4.2.1 Survey Section A

Survey responses from experts indicated a positive reception to the game's design and functionality. Ratings were as follows:

- **Graphics**: Good or Excellent
- **Gameplay**: Positive feedback overall
- **Audio and Sound Effects**: Average
- **Shopping, Timer, AI**: Mixed reviews
- **Appeal to Ages 3-6**: Average to Good

The game received 13 "Average," 27 "Good," and 7 "Excellent" ratings, indicating general approval but also areas for improvement.

#### 6.4.2.2 Survey Section B

Participants highlighted benefits of multiple wireless devices, though concerns included battery life and setup complexity. The data analysis feature was particularly appreciated for monitoring progress and therapy adjustments.

#### 6.4.2.3 Research Limitations

- Only a mobile-device interface was developed as a wireless device.
- Additional cycles of evaluation could enhance rigour.
- In-person surveys would provide more comprehensive feedback.

### 6.4.3 Conclusion

This appendix provides an overview of the artefact development and research, covering:

- Artefact description
- Life cycle and phases
- Development process
- Research methodology
