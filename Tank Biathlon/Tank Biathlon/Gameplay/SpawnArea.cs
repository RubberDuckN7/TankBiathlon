using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Iris;

namespace Tank_Biathlon
{
    public class SpawnArea
    {
        private class GridTile
        {
            public Vector2 pos = Vector2.Zero;
            public bool taken = false;
        }

        private GridTile[][] grid;
        private float offset;
        private float tile_size;

        public SpawnArea(float screen_width, int count_w, int count_h, float tile_size)
        {
            this.tile_size = tile_size;
            grid = new GridTile[count_w][];

            float x = (float)(screen_width - tile_size*count_w) * 0.5f;
            for (int i = 0; i < count_w; i++)
            {
                grid[i] = new GridTile[count_h];

                float y = -((float)((count_h + 1) * tile_size));
                for (int j = 0; j < count_h; j++)
                {
                    grid[i][j] = new GridTile();
                    grid[i][j].pos = new Vector2(x, y);
                    grid[i][j].taken = false;
                    y += tile_size;
                }

                x += tile_size;
            }
        }

        public Vector2 Spawn()
        {
            Vector2 sp = new Vector2();

            bool res = false;
            while(!res)
            {
                //int x = (int)Tools.Random(0, grid.Length);
                //int y = (int)Tools.Random(0, grid[0].Length);

                int x = (int)Tools.Random(0, 100f) % grid.Length;
                int y = (int)Tools.Random(0, 100f) % grid[0].Length;
                bool valid = SearchRows(y);
                if (valid && !grid[x][y].taken)
                {
                    res = true;
                    sp.X = grid[x][y].pos.X;
                    sp.Y = grid[x][y].pos.Y+offset;
                    grid[x][y].taken = true;
                }
            }
            return sp;
        }

        public bool SearchRows(int id_y)
        {
            byte count = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i][id_y].taken)
                    count++;
            }

            return count < 2;
        }

        public void Move(float step)
        {
            offset += step;

            if (offset > tile_size)
            {
                Tick();
            }
        }

        private void Tick()
        {
            UpdateGrid();
            offset = 0f;
        }

        private void UpdateGrid()
        {
            for (int i = grid.Length - 1; i > 0; i--)
            {
                for (int j = grid[i].Length - 1; j > 0; j--)
                {
                    if (j == grid[i].Length - 1)
                    {
                        grid[i][j].taken = false;
                    }
                    else
                    {
                        if (grid[i][j - 1].taken)
                        {
                            grid[i][j].taken = true;
                            grid[i][j - 1].taken = false;
                        }
                    }
                }
            }
        }
    }
}
