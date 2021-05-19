# Dungeon Explorer

Personal game project made with Unity. The project was made to meet the requirements of a college course, but was further developed to train my Unity skills.

## IMPORTANT DISCLAIMER

I am very aware that the code written in this project is not optimal.
I use a lot of FindObjectsWithTag() and GetComponent<>() at runtime, and probably even in the Update method invoked every frame.
It doesn't make a huge difference in performance in a project of this scale, but it is important to acknowledge that this is a bad way of doing this.
Many things in the code are not very elegant or intelligent, that was the level of Unity C# I had at the time of developing this project.
I've made many improvements in the way I write Unity code now.

- ### So why keep this project on display?

This project was my first "bigger" prototype I created using Unity, I did a lot of the content from scratch, including personally created pixelart sprites and animations.
Although not very good, they were my first attempts in pixelart and animation and I value this greatly.
I put a lot of work in this project and I was very proud of waht I accomplished back when I finished it.

## What is it about?

You control a fox and explore a random generated dungeon full of hostile slimes in a desperate attempt to get to the next floor!
When you enter a new room and it has enemies inside, the doors lock until you defeat your last foe. The same is true for the hatch leading to the next floor.
The game loops and keeps track of your score as you fight your way through the dungeon and it's mysteries. 
Each floor is generated randomly, creating an end room with a hatch somewhere. 
