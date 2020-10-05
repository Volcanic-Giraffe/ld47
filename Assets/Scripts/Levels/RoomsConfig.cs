﻿public class RoomsConfig
{
    /**
     * 0 - пусто,
     * 1 - стена,
     * 2 - 100% EnemySentry,
     * 3 - 100% BigBot,
     * 4 - 100% EnemyCart,
     * 5 - 100% HeartPickup,
     * 6 - N% шанс EnemySentry,
     * 7 - N% шанс BigBot,
     * 8 - N% шанс EnemyCart,
     * 9 - N% шанс HeartPickup,
     */

    /*
     * No walls here:
        
        {x,x,x,x,0,0,0,0,0,0,0,0},
        {x,0,0,0,0,0,0,0,0,0,0,0},
        {x,0,0,0,0,0,0,0,0,0,0,0},
        {x,x,x,x,0,0,0,0,0,0,0,0}
        
        to avoid dead ends
     */
    public static int[,,] RoomVariants =
    {
        /*
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        */
        // DEBUG ONE
        {
            {0,0,0,0,1,0,2,0,0,0,0,0},
            {0,0,0,0,1,0,5,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,1,1},
            {0,0,0,0,0,2,0,0,0,0,1,1}
        },
        {
            {0,0,0,0,0,0,0,0,0,0,0,0}, // simpler set
            {0,0,1,1,6,0,0,1,1,0,0,0},
            {0,0,1,1,6,0,0,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,0,0,0,0,0,0,0,0}, 
            {0,0,0,2,1,0,0,1,6,0,0,0},
            {0,0,0,1,1,0,0,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,1,3,1,3,1,3,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,1,3,1,3,1,3,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,3,0,1,0,1,0,0,0,0,0},
            {0,1,0,2,0,0,0,0,0,0,1,0},
            {0,1,0,0,0,0,0,0,5,0,1,0},
            {0,0,0,0,1,0,1,0,0,0,0,0}
        },
        {
            {0,0,0,0,1,2,0,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,1,0,2,1,0,0,0,0}
        },
        {
            {0,0,0,0,1,5,0,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,1,0,4,1,0,0,0,0}
        },
        {
            {0,0,0,0,1,0,0,1,0,0,0,0},
            {0,0,0,0,0,3,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,1,0,0,1,0,0,0,0}
        },
        {
            {0,0,0,0,0,0,2,1,0,0,0,0},
            {0,0,0,0,1,0,0,0,0,0,0,0},
            {0,0,0,0,1,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,1,0,0,0,0}
        },
        {
            {0,3,0,0,0,0,0,0,0,0,0,0}, // Dancefloor
            {8,4,3,7,0,0,0,0,0,0,0,0},
            {8,8,3,7,0,0,0,0,0,0,0,0},
            {0,3,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,0,0,1,0,4,0,0},
            {0,0,1,0,0,0,0,1,0,8,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,0,0,0,0,3,0,0,0},
            {0,0,0,0,0,0,0,0,3,4,8,0},
            {0,0,0,0,0,0,0,0,3,4,8,0},
            {0,0,0,0,0,0,0,0,3,0,0,0}
        },
        {
            {0,0,0,0,1,1,1,1,1,0,0,0}, // Blockades
            {0,0,0,0,0,7,0,0,0,0,0,0},
            {0,0,0,0,0,7,0,0,0,0,0,0},
            {0,0,0,0,1,1,1,1,1,0,0,0}
        },
        {
            {0,0,0,0,1,1,1,1,1,1,1,0},
            {0,0,8,3,7,0,0,0,0,0,1,0},
            {0,0,0,3,7,0,0,0,0,0,1,0},
            {0,0,0,0,1,1,1,0,0,0,0,0}
        },
        {
            {0,0,0,0,0,2,1,6,0,0,0,0}, // Labyrinth
            {0,0,0,0,1,0,1,0,1,0,0,0},
            {0,0,0,0,1,0,1,0,1,0,0,0},
            {0,0,0,0,1,0,0,0,1,0,0,0}
        },
        {
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,1,6,1,1,1,1,1,1},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
       
        {
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,2,1,1,1,0,0,1,1,1,2,0},
            {0,1,1,1,8,0,0,9,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,2,1,1,1,0,0,1,1,1,2,0},
            {0,1,1,1,8,0,0,9,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,1,1,1,1,0,0,0,0}, // Treasure 1
            {0,0,0,2,1,0,5,0,2,0,0,0},
            {0,0,0,2,0,5,0,1,2,0,0,0},
            {0,0,0,0,1,1,1,1,0,0,0,0}
        },
        {
            {0,0,0,0,5,5,5,5,0,0,0,0}, // Treasure 2
            {0,0,0,0,5,9,9,5,0,0,0,0},
            {0,0,0,0,5,9,9,5,0,0,0,0},
            {0,0,0,0,5,5,5,5,0,0,0,0}
        },
        {
            {0,0,0,0,0,0,0,2,0,0,0,0}, // Turret lines
            {0,0,1,0,0,0,0,2,0,0,1,0},
            {0,0,1,0,0,0,0,2,0,0,1,0},
            {0,0,0,0,0,0,0,2,0,0,0,0}
        },
        {
            {0,0,0,0,6,0,0,2,0,0,0,0},
            {0,0,0,0,0,0,0,2,0,0,0,0},
            {0,0,0,0,0,0,0,2,0,0,0,0},
            {0,0,0,0,6,0,0,2,0,0,0,0}
        },
        {
            {0,0,0,0,6,0,0,2,0,0,0,0},
            {0,0,0,0,0,0,0,2,0,0,1,0},
            {0,0,1,0,0,0,0,2,0,0,0,0},
            {0,0,0,0,6,0,0,2,0,0,0,0}
        }

    };
}