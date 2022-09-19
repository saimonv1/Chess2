import { useState } from 'react';

import classes from './Tile.module.css';

const Tile = (props) => {
    const [isPressed, setIsPressed] = useState(false);

    const onClickHandler = () => {
        setIsPressed(prevState => !prevState);

        console.log('X: ' + props.row + ' Y: ' + props.node);
    };

    return (
        <div onClick={onClickHandler} className={`${classes.tile} ${isPressed ? classes.pressed : ''} ${props.obstacle ? classes.obstacle : ''}`}></div>
    );
};

export default Tile;