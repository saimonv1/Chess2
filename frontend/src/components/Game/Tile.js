import { useContext, useState } from 'react';
import GameContext from '../../store/game-context';

import classes from './Tile.module.css';

const Tile = (props) => {
    const [isPressed, setIsPressed] = useState(false);

    const gameCtx = useContext(GameContext);

    let colClass;

    if(gameCtx.color === 'red'){
        colClass = classes.red;
    }

    if(gameCtx.color === 'green'){
        colClass = classes.green;
    }

    if(gameCtx.color === 'blue'){
        colClass = classes.blue;
    }

    if(gameCtx.color === 'yellow'){
        colClass = classes.yellow;
    }
    
    const onClickHandler = () => {
        setIsPressed(prevState => !prevState);

        console.log('X: ' + props.row + ' Y: ' + props.node);
    };

    return (
        <div onClick={onClickHandler} className={`${classes.tile} ${isPressed ? colClass : ''} ${props.obstacle ? classes.obstacle : ''} `}></div>
    );
};

export default Tile;