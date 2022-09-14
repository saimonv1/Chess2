import Tile from "./Tile";

import classes from './Board.module.css';

const GRID_ROW_LENGTH = 20;
const GRID_COL_LENGTH = 20;

const Board = (props) => {
    const grid = [];
    for (let row = 0; row < GRID_ROW_LENGTH; row++){
        const currentRow = [];
        for(let col = 0; col < GRID_COL_LENGTH; col++){
            currentRow.push('');
        }
        grid.push(currentRow);
    }

    return (
        <div>
            <h1>Slapyvardis: {props.userName}</h1>
            <div className={classes.grid}>
                {grid.map((row, rowId) => {
                    return (
                        <div key={rowId} className={classes.row}>
                            {row.map((node, nodeId) => {
                                return (
                                    <Tile key={nodeId} row={rowId} node={nodeId}/>
                                );
                            })}
                        </div>
                    )
                })}
            </div>
        </div>
    );
};

export default Board;