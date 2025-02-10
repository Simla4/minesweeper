I did this project to learn and apply the flood fill algorithm in more depth.

## Flood Fill Algorithm

The Flood Fill algorithm is used to fill a region by expanding from a starting point. It is commonly used in 2D games, image editing tools (e.g., the "bucket fill" tool), and games like Minesweeper to reveal empty areas.

In Minesweeper, if a clicked cell does not contain a mine and has no adjacent mines, the algorithm reveals the surrounding empty cells recursively until it reaches cells wit

## Algorithm Logic
1. Select a starting cell – The algorithm begins from the cell clicked by the player.
2. Check conditions – If the cell is already revealed, contains a mine, or is out of bounds, the algorithm stops.
3. Reveal the cell – If the cell has no adjacent mines, it spreads to its neighbors.
4. Continue expanding – The process repeats recursively, revealing all connected empty cells.

[Movie_003.webm](https://github.com/user-attachments/assets/12e32211-b700-4089-840f-3d31db3f282a)



