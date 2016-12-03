# IMT1361 ­ Game Design ­ Assignment 3

![image alt text](http://i.imgur.com/3tsezwD.png)

Members

* Stian   	15%

* Halvor	25%

* Håkon H.	15% 

* Edvard	15%

* Christian 	15%

* Håkon N.	15%

[Gameplay Video]		      [https://www.youtube.com/watch?v=KoEsu2TFOV4](https://www.youtube.com/watch?v=KoEsu2TFOV4)

[Executable & Source Code]    [https://github.com/Stektpotet/Amazeking/releases/tag/1.0](https://github.com/Stektpotet/Amazeking/releases/tag/1.0)

Contents

* * *
### [Assignment 3](#assignment-3-1)
#### 1. [Concept Document](#1-concept-document)
1.1 [The Idea Of The Game](#11-the-idea-of-the-game)
1.2 [The Genre/Type Of Game](#12-the-genretype-of-game)
1.3 [Other Similar Games](#13-other-similar-games)
1.4 [What Makes This Game Interesting/Different](#14-what-makes-this-game-interestingdifferent)
#### 2. [Design Document](#2-design-document-1)
2.1 - [Technology Description](#21---technology-description)
2.1.1 - [GraphicsGale](#211---graphicsgale)
2.1.2 - [Adobe Photoshop CS5](#212---adobe-photoshop-cs5)
2.1.3 - [Unity](#213---unity)
2.1.4 - [Fruity Loops Studio (FL Studio)](#214---fruity-loops-studio-fl-studio)
2.2 - [Flow of the game](#22---flow-of-the-game)
2.2.1 - [Game Flow - Flow Chart](#221---game-flow---flow-chart)
2.3 - [Game Play Description & Game Mechanics](#23---game-play-description--game-mechanics)
2.3.1 - [Movement and interaction](#231---movement-and-interaction)
2.3.2 - [Items](#232---items)
2.3.3 - [Puzzle mechanics](#233---puzzle-mechanics)
2.3.4 - [Thinking](#234---thinking)
2.4 - [Discussion On Balance Of Scoring And Mechanics](#24---discussion-on-balance-of-scoring-and-mechanics)
2.5 - [Assets](#25---assets)
2.5.1 [Sprites](#251---sprites)
2.5.2 [Sounds](#252---sounds)
#### 3. [Analysis](#3-analysis-1)
3.1 [Lenses](#31-lenses)
3.2 [Story elements](#32-story-elements)
3.3 [Connection to lecture content](#33-connection-to-lecture-content)
* * *
### [Assignment 4](#assignment-4-1)
#### 1. [Discussion of feature changes, and why they changed](#1-discussion-of-feature-changes-and-why-they-changed-1)
1.1.1 [Overall](#111-overall)
1.1.2 [Graphic](#112-graphic)
1.1.3 [Programming](#113-programming)
1.1.3.1 [Tools](#1131-tools)
1.1.3.2 [UnityEvents](#1132-unityevents)
1.1.3.3 [Bodging](#1133-bodging)
1.1.3.4 [Other](#1134-other)
1.2 [Play testing](#12-play-testing)
1.3 [Strengths and weaknesses](#13-strengths-and-weaknesses)
#### 2 [Analysis of the game](#2-analysis-of-the-game-1-2-pages-1)
2.1 [Lecture content](#21-lecture-content)
2.2 [Where appropriate which lenses are most relevant for the development](#22-where-appropriate-which-lenses-are-most-relevant-for-the-development)
2.3 [In relation to what we intended to make at the beginning of the project](#23-in-relation-to-what-you-intended-to-make-at-the-beginning-of-the-project)

* * *

### Assignment 3

# 1. Concept Document

## 1.1 The Idea Of The Game

In a world ruled by impulsive and stupid kings, waging war for love and killing thousand out of ignorance, wise counselors to a king made a maze. The kings maze were made to find the smartest most calculating candidates for the position of king. First all the nobles of the land came to try the maze, but non could get through. Then the middle class came but neither any of they could solve all the puzzles within. Then at last the peasants got their turn. It is here, you came to stand in line, in your early twenties you took your place in line, and now, in your late fifties, it is finally your turn, will you solve the maze? Like a just and trustworthy ruler or maybe like a grudgeful and unsavory tyrant?  Who knows, maybe you will unlock the mysteries of the maze.  		

The kings maze is comprised of multiple puzzles/levels where you have to solve them to get ahead to the next level, the catch is that all the puzzles are "incomplete" and “unsolvable”. They are missing one or several elements making their “original” solution obsolete.

You, the player, as the somewhat sadistic peasant must find alternate solutions to progress towards dominion. The alternate solution(s) will involve the blood of your enemies, the groundskeepers, be it tossing their limbs onto buttons or stacking them to create stairs, you will proceed.

## 1.2 The Genre/Type Of Game

The King’s maze is a puzzle-platformer/point ‘n click adventure. In this game you move your character around the map to figure out the different puzzles. You can use force to obstacles with your character. The obstacles might help you solve the puzzles.

## 1.3 Other Similar Games:

Braid - Time-warp to make levels solvable (PC/Steam).

![image alt text](http://i.imgur.com/bviyD6S.jpg)

Fireboy and watergirl - Buttons, door(for additional rooms).

![image alt text](http://i.imgur.com/VWr39ZA.png)

## 1.4 What Makes This Game Interesting/Different

The original puzzles are incomplete. You can only solve them by using elements that were not originally planned to be part of the puzzles. The player is not the traditional hero, he will kill peaceful beings to succeed.

Feel of the game:		

![image alt text](http://i.imgur.com/qOI6gWk.gif)

# 2. Design Document

## 2.1 - Technology Description

#### 	2.1.1 - GraphicsGale

* GraphicsGale is a drawing tool used for animating and creating sprites and textures. It’s free and it’s very good for making pixelated sprites and textures. You can preview the animations while creating them, improving the workflow.

#### 2.1.2 - Adobe Photoshop CS5

* Adobe Photoshop is also a graphics tool, this we’ll use for more advanced textures and sprites where the alpha channel is needed. The workflow between photoshop and unity is also very nice as the .psd format is supported by Unity. Photoshop is not free, but as we have access to trial versions, it is usable.

#### 2.1.3 - Unity

* Unity is our chosen game engine. Though it was originally made for 3D-games, it’s 2D support has come a long way. We have chosen to use Unity as it’s relatively easy to use, both for beginners and more experienced users, the basic framework for building games is there already (collision and physics simulation etc.), and their focus on workflow and usability for developers, both artists and programmers is present. Unity is free for personal use and for indie developers given that the product does not sell for more than US$ 100.000.

#### 2.1.4 - Fruity Loops Studio (FL Studio)

* FL Studio is a DAW (digital audio workstation) making it a very extensive product for this kind of project. The reason we’ll be using this is that it’s very easy to make simple tunes quickly with the default sound packs. It’s not a tool we’ll all use, but it will be used by our *composer* (Håkon) who is familiar with the use of it. It’s not free, but as our *composer* already owns the program we’ll use it.

## 2.2 - Flow of the game

The menu will be where you can start, quit and go to options from. 

If you die in a room a restart level/main menu display will show up.

The menu will be like an overlay, sliding in from the left and pausing the game. 

We think the level selection will work like in the mario games for gameboy, where you are and are transported to the levels trough level selection.

This allows for the possibility of playing through levels you’ve already completed, while also allowing the player to progress in the game. The design of this "world map" will also be important as it is the main indicator for how far the player has come in the game.

### 2.2.1 - Game Flow - Flow Chart

###  ![image alt text](http://i.imgur.com/hjlEnQB.png)

[https://drive.google.com/file/d/0BxUcE3-IlSh2WkF5Rlp3bG0xd1k/view?usp=sharing](https://drive.google.com/file/d/0BxUcE3-IlSh2WkF5Rlp3bG0xd1k/view?usp=sharing)

## 2.3 - Game Play Description & Game Mechanics

#### 2.3.1 - Movement and interaction

The movement in the game is based around the wasd keys. A og D is for moving left and right. Space is used for jumping. To interact in the game we have planned to use the E key. You can now also kick with ‘E’ and punch with ‘Q’. W and S is used for climbing upwards and downwards on ladders or ropes. To crouch press C to toggle crouch on and off. 

#### 2.3.2 - Items

Item drops are an essential part of solving some of the puzzles. They will mostly be found on corpses of either groundskeepers that you’ve killed, or previous contestants for *the trial...* These items will not be typical items found on anyone, but rather unique items that will fit for the current puzzle, similar to point and click adventures.

#### 2.3.3 - Puzzle mechanics

The main mechanic in our game is going to be killing the groundskeepers. Therefore there are a lot of mechanics based around the groundskeepers. 

The groundskeepers can be punched/kicked and then pushed to the side in the room instead of picked up. They can be used for buttons to open a door for the next room.

One mechanic is that after the groundskeepers are dead you can use their body parts. You can pick them up and place them/ interact with them. When killing the groundskeepers you can also drain their blood, this blood will be used to fill up buckets so that their weight is increased. The buckets can after that be placed on weights to for example lift or open something. 

If you don’t remove any body parts from the groundskeeper you can carry it around. You can then use the body to place on buttons on the floor. The buttons on the floor can stop darts or arrows for beeing shoot. There are also other buttons that you can just press and the darts or arrows just stop for some seconds. When picking and placing the groundskeepers you can also place them to get a height advantage when you need to jump, you can place several groundskeepers on top of each other. Another way to get higher in the game is to use ladders or rope. You get rope from the groundskeepers intestines, you can throw the intestines on hooks to climb up. 

Groundskeepers are placed throughout the levels but some are in cages, to release the groundskeepers you need a key. The keys are found in the levels. 

#### 2.3.4 - *Thinking*

*"Thinking"* is a player ability that will either be like a super power that can be used a certain amount of times, costing something for each use. Or it will be an uncontrolled action happening as a reaction of no progress for a while. Basically the idea is to help the player along if he gets stuck. This is of course a mechanic that will have to be balanced correctly, as we don’t want it to be like us holding the hand of the player through the entire game.

2.4 - Discussion On Balance Of Scoring And Mechanics

With the style of our game we found out that we wouldn’t spend much time working on scoring, as this won’t fit our game very well. Instead of scoring we have thought about giving the players stats at the end of the game. 

The game starts with easy puzzles that introduce the player to both controls and puzzle mechanics. Once it’s reasonable to think that the player has familiarized himself with the mechanics, we’ll mix things up by combining the different mechanics.

We added a timer both as a fun stat and you can use it to compete against other players.

## 2.5 - Assets

#### 2.5.1 Sprites

* Main character ->

* Groundskeeper child

* Dead groundskeeper in half, both ends (torso and head, legs)

* Destroyed boxes

* Spikes

* Darts

* Trees

* Bookshelfs

* Windows

* Door

#### 2.5.2 Sounds

* Music

    * Background

    * Main menu

    * Cutscenes

* Killing groundskeeper child

* Finishing a level

* Idle sounds (Scratching of beard, "hmmmm", etc.)

* Door

* Steps

# 3. Analysis

## 3.1 Lenses

	

The main lens we focused on when designing is The Lens of the Puzzle as it captures the essence of the game. We tried to follow the ten puzzle principles and all puzzle lenses closely. Principle 8 says that we should give hints, and we will do that if the player is stuck on a level or a problem. The hints should not be a straight up solution, but help the player think in a certain direction. Principle 10 perceptual shift is something we will try to avoid having too much of, simply because we want players to feel more progress towards the goal.

The accessibility lens showed us that we should have tutorial level to show players the mechanics and help them solve the first puzzles as it might be a bit tricky. As the later levels will have a lot of steps the visible progress lens is important. Players will see them getting closer to the target by finishing one problem at a time, and it will be clear that you come closer to the goal. The lens of the pyramid is the baseline of our game as we will introduce different mechanics across the levels. After learning a mechanic players will then play around with that for a bit, then combine it with other mechanics from earlier levels. The last levels will include almost all the puzzles players have tried out.

Other lenses that is not puzzle lenses include the lens of punishment as the player might be stuck on a puzzle or do something that prevent them from moving on. You will then have to restart the level from the beginning, but hopefully you will learn by doing it. Restarting should feel fair because you know what caused it and prevent it by learning. If we have a long level we might add checkpoints to stop grinding, as this feel tedious and get you bored of the game. The lens of skill is for our game mostly problem solving and recognising patterns. The skills can be trained, but if the puzzles are too easy or hard the game might not seem fun. The lens of challenge also touch on the same topics, and that we need to have variety in our challenges. 

## 3.2 Story elements

There will be an underlying story in the game. The Council which is ruling the kingdom at the moment are corrupted. The story is in a way the backbone for all of the level setting. The puzzles are "incomplete" both because of the time passed, and because the Council have made them impossible to solve. This will especially be the case in the last levels, where it’s clear that the puzzles should not in any way be solvable. In addition you can, by exploring parts of the game, encounter clues to what has actually been going on: **_Blood on the wall _**- “*Do not trust the council!*”. In some levels there may be doors leading into other rooms. In these rooms you may find old notes or books revealing more of the story. 

As it’s only the first few levels we have implemented yet there isn’t many clues about the darker main story.

## 3.3 Connection to lecture content 

The most important that we learned from a lecture regarding puzzle games was to look at other good and bad games. We did this early in the process and we could see what makes a bad puzzle game. We also included some mechanics from games we thought were good and what we would like to have in our game. We also took note of the graph showing how difficult the puzzles should be in that there should be a spike, and flatten out then another spike.

### Assignment 3

## 1. Discussion of feature changes, and why they changed

### 1.1.1 Overall:

We decided early on to "split" the group into a design group and a programming group. This because we wanted to utilize everyone to their best potential. We think there is both pros and cons to doing this. We also talked to a app group that had done kind of the same, but they had some problems with bad communication between their two groups and therefore had to wait for one another to be finished with their assigned tasks. So something we made sure to watch out for from the beginning is that both designers and programmers worked on “the same page” and that neither of the groups fell behind on their tasks. We started out with three people in both groups, but after seeing that two people could handle the drawing of the pixel animations by them self one person was sent over to the programing group to ease their workload. 

### 1.1.2 Graphic:

The art style was decided on early and that is pixel art. We wanted to make as much of the game ourselves, therefore all the pixel art is drawn by us. We in the design group went for a medieval look with also a dash of sinister, and fun mixed inn. How we have tried to do this is by having a shadow over the main characters face, and give him yellow piercing eyes. We also made the world a dungeon’ish castle. The fun/quirky side of the game is something we have tried to draw out by making the players movements look a bit over exaggerated and "off". The design group has worked hard to try and make the art our own and also to look good. We decided on a story and mechanics early on, and after that gave the design guys free rains on how the art itself should look. None of us have any prior experience with drawing and therefore we used  some time in the beginning to get used to drawing pixel art. Mostly by watching youtube videos on drawing, we  also tried to stay away from playing any pixel games, so as to keep our own design “our own”, without any interference from outside sources.        

### 1.1.3 Programming:

The programming of this game can be split into three parts:

##### 	1.1.3.1 Tools

Unity is built upon the principle of allowing the users to create their own tools. Beginning this project we saw the potential to code some small editor tools to speed up the actual world design. One of the tools/systems is a camera tool/system making tweening between level "stages" or level “targets” easy. It also made sure the view was clamped (both size and position) within the play area to only show the wanted areas of a map at any given time. This system was built to work both in runtime and edit-time. In edit-time it’s a tool as it draws custom handles and gizmos for resizing level areas visually. It also allows for quick-moving the camera to the correct position (focusing on the customly set target, being the player in most cases).

![image alt text](http://i.imgur.com/Q3LvEQG.png)![image alt text](http://i.imgur.com/Cp5Wwtu.png)

Another tool we made was a tool for blending textures. This wasn’t intentionally planned, but was rather a result of irritation of the stupidity of Unity sprites; by default they’re set to "clamped" wrapping mode, making the texture not repeat infinitely if you stretch it. Stretching the default sprites caused another irritation, as their UV wrapping is set to stretch with the sprites game objects, which made background walls really irritating. All this irritation caused us to look into shaders, as they in some ways were the original problem. We looked into the standard Unity shaders for sprites which are written partially in GLSL, and realized we could do a lot with their power. 

We started simple by making a sprite shader that took the world coordinates as UV wrapping. This made the background walls a lot easier to make, as we could simply make a large, repeating background. At this point we started tinkering with the idea of a focus on the aesthetics of the game. We really wanted to make a nice looking game, so we added normalmaps to make elements be affected by lights in a more natural way.

The next step was the idea of blending textures. We already had multiple variants of the background wall, but it didn’t look very natural in the way they transitioned when they were on separate sprites. So we added an RGBA texture parameter in the shader, using each color as a mask for four overlaying textures. The only problem here was the fact that we had no way of painting this RGBA-texture within unity... yet. *Alphabrush *is a tool we made specifically for textureblending 5 textures with their normalmaps:

 ![image alt text](http://i.imgur.com/3sMhuT7.png)![image alt text](http://i.imgur.com/L1oRoAO.png)![image alt text](http://i.imgur.com/jxkQbKj.png)

##### 1.1.3.2 UnityEvents

UnityEvent is a really powerful class within the Unity standard libraries. It’s basically like C#’s delegates for setting up callback functions. However it’s serialized and assigned from the Unity-inspector. This meant that all code could largely be kept within their respective gameobjects and callback functions between the different objects would handle the logic of their relations. An example of this is our  trigger system:

This made a lot of our basic mechanics easy to implement, but it also had the negative effect of being really difficult to debug as all relations were fairly abstract.

##### 1.1.3.3 Bodging

In the end, just to make sure everything worked as intended, we started patching up the pieces of code. This is where we saw that the usage of UnityEvents in most objects was not really desirable. As mentioned above, debugging for errors with them is difficult, making it difficult to make fixes for bugs too. So in some cases, especially in the structures made in the end of development, we bodged code into places where they didn’t really belong just to make fixes more accessible through unity’s interface.

##### 1.1.3.4 Other 

We made the buttons ‘E’ and ‘Q’ the interact-buttons. They will allow the player to kick or punch the different objects or actors (like the groundskeeper child) around the map, making an alternative choice to a solution of levels. This was added instead of an UI button display where mouse input was needed. It was really desired by the people playing in the playtest to be able to ditch the mouse entirely. We added a display for restarting the level if so is needed or if the player wants to quit the game. This way you don’t need to restart from the first level if you die on the last. We haven’t added a level selection yet, but instead have every level come in an order after each other. The game also contains a timer now. This will make players urge for more than one playthrough, and therefore make the game more of a challenge/puzzle. From the main menu we also added a button for credits for the players to know who made this fantastic game!

## 1.2 Play testing

During our playtest we had some players getting stuck at level 3 and wanting to quit the game. They felt level 3 was too different and that the difficulty spiked too and that we could have introduced the spikes in another easier way.

Another thing that the players thought was annoying/improvable was that you had to click to kick and punch. The players wanted this to be binded to a key instead of having to click E and then drag the cursor over and click on the action you wanted to do. 

We therefore changed the bindings too Q and E. 

Some playtesters also gave us feedback that the walking in the start of level 1 was too long and bored them. We felt the start of the game introduced the player to the mood in the game, that the main character in the game had to travel a long way before arriving at the maze. 

We also got feedback from players that thought the simplicity of the game was nice and that the easy mechanics made it fun to play around with. They also wanted to explore the levels and see if you could find other ways too complete the level. This also included trying to speedrun the levels too find the most efficient way to do the levels. This showed us that the game had replayability. The average first playthrough time was 3-5 minutes and the players then tried to get the fastest time. Therefore we wanted to add something to show how good the players were doing. We chose to do this with a timer. 

## 1.3 Strengths and weaknesses:

Strengths:

Even though this game is just a demo the technology is pretty solid. This will make it easier to develop more levels later. We can also use our current assets in the future as a part of a pyramid of puzzles. The visuals are good with both pixel graphics and lighting/shadows. 

Weaknesses: The game is only 3 levels long, and will only take a couple of minutes to complete. There is only a few mechanics we were able to show  as first levels can’t be too complicated.

On level 2 you and items will start to float while nearing the corners, this is because the crate could be stuck with no way to get it out.

Known bugs: Stuck in jump animation after jumping from a bookshelf or crate. Freezing character if you press E too fast to skip text. Glitching when you walk into a pile of boxes(level 3). The shadow when you walk to the left will create a white box behind the back. You can jump on the side of boxes while in the air. The collider in level 2 on the groundskeeper is too big. The groundskeeper may then lie to the right of the button while the button is pressed down. The timer will remain in the top right corner when you return to the main menu and when you start a new normal game. The groundskeeper may also be stuck in the falling animation when lying on the floor. In level 3 when you kick the groundskeeper down on the spikes sometimes you will go straight through the groundskeeper and be killed by the spikes.      

# 2 Analysis of the game

## 2.1 Lecture content

The most important thing that we learned from a lecture regarding puzzle games was to look at other good and bad games. We did this early in the process and we could see what makes a bad puzzle game. We also included some mechanics from games we thought were good and what we would like to have in our game. We also took note of the graph showing how difficult the puzzles should be in that there should be a spike, and flatten out then another spike.

## 2.2 Where appropriate which lenses are most relevant for the development

The lens of problem solving: We based our game based puzzles. The puzzles in the game are designed to be solved in two possible ways but having the same main goal. We wanted the game to be torn into two main paths you can go so that players could come back and try to finish the game in an other way, either killing the groundskeepers or dodging around the kills and finishing the levels without killing anyone. 

The lens of challenge: The challenge of our game is to get to end of each level and ultimately becoming king when you have successfully done all the challenges. The challenges we have made for our game at this point are fairly easy. This is because we started at the start of the game. We wanted the player to get familiar with how the game works before starting with harder challenges. Our plan was to implement harder challenges further into the game but keeping the core aspect of the game, killing/saving the groundskeepers. Because we have prioritised making the first levels good there are maybe not enough variety in our challenges. When visioning a finished product it would have more game mechanics and bigger variety in the levels.  

The lens of the avatar: In the development of the game the creation of the avatar was important. We feel the avatar is representative for the time and place where the story is taking place and that the players will think that the avatar connects good with the story. We also made him faceless so that the player could connect better with the character. We also tried to features that made him look old like his beard and clothes. 

## 2.3 In relation to what you intended to make at the beginning of the project

**What we thought in the beginning and what we made:**

There has been done a lot of changes to the original idea of the game. But what brought most of the changes was not a change of idea, but more that we overestimated our ability to what we can make in such a short period. What we ended up planning was in itself close to a full fledged game, and certainly not something we had time to make in three weeks. Something we also think we should have done differently is starting from the middle out. Had we started making the game from the middle we would have been done with a lot of the planned mechanics early on, instead we started from the start and made the tutorial. And therefore we did not have time to make a lot of what we originally had planned. While the programmers did their part, the two people assigned to design spent a lot of time drawing the different sprites, so the result became a lot more graphics based game than we first intended. Not that we think that is a bad thing, and we also saw that if we had more time, the good groundwork we did making the art would have made it easier to implement more of it later. 

Our original idea of the NPCs were that they were going to be cute minions. We found it difficult to make these minions and therefore changed them into groundskeepers. This also made the story more believable. We also did not make the extra rooms to tell the story we wanted because we prioritized making the game mechanics work and polishing the levels so that they looked nice instead of working on the story. We also overestimated how big the levels where going to be and how many puzzles there were going to be in level one. We therefore changed level one two be more of a tutorial level where you only learned  the basic movement and interaction and not starting with the puzzles until further in the game.

