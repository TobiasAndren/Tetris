using System;

namespace Tetris;

public class BlockQueue
{
    private readonly Block[] _blocks = new Block[]
    {
        new IBlock(),
        new JBlock(),
        new LBlock(),
        new OBlock(),
        new SBlock(),
        new TBlock(),
        new ZBlock()
    };
    
    private readonly Random _random = new Random();
    
    public Block NextBlock {get; private set;}

    public BlockQueue()
    {
        NextBlock = RandomBlock();
    }

    private Block RandomBlock()
    {
        return _blocks[_random.Next(_blocks.Length)];
    }

    public Block GetAndUpdate()
    {
        Block block = NextBlock;

        while (block.Id == NextBlock.Id)
        {
            NextBlock = RandomBlock();
        }

        return block;
    }
}