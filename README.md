# **3D Chess Game**
This project is a 3D chess game developed in Unity using the C# programming language.

## **Description** 
The game is played between two entities (bots), representing the black and white sides, which compete against each other. Each bot calculates its moves for every turn using a decision tree algorithm (Minimax with Alpha-Beta Pruning). The algorithm, combined with a heuristic function, determines the optimal move at each stage of the game.

In addition to the bots, a third player (the user) controls a character called the "fly." The objective of the fly is to survive the battle between the two bots. The fly can move one step per turn in any direction (forward, backward, left, right, or diagonally).

The player loses if the fly is captured by one of the bots. To win, the fly must survive until the end of the battle between the black and white bots.

## **Key Features**
* **AI-Driven Bots:** Each bot uses a Minimax algorithm with Alpha-Beta Pruning for efficient decision-making.
* **Interactive Gameplay:** The player controls the fly, introducing a unique twist to traditional chess.
* **Survival Mechanic:** The bots do not account for the fly's presence in their move calculations, making survival both challenging and strategic.
