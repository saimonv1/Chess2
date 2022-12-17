import { useContext, useState } from 'react';
import GameContext from '../../store/game-context';

import classes from './Tile.module.css';

const Tile = (props) => {
    const [isPressed, setIsPressed] = useState(false);

    const gameCtx = useContext(GameContext);

    let colClass;
    if(gameCtx.color === 0){
        colClass = classes.red;
    }

    if(gameCtx.color === 2){
        colClass = classes.green;
    }

    if(gameCtx.color === 1){
        colClass = classes.blue;
    }

    if(gameCtx.color === 3){
        colClass = classes.yellow;
    }
    
    let unitClass = '';
    if(props.unit) {
        debugger;
        if(props.unit.color === 0){
            if(props.unit.isDestroyed){
                debugger;
                unitClass = classes.redwreck;
            }
            else {
                unitClass = classes.redtank;
            }
        }
    
        if(props.unit.color === 2){
            if(props.unit.isDestroyed){
                debugger;
                unitClass = classes.greenwreck;
            }
            else {
                unitClass = classes.greentank;
            }
        }
    
        if(props.unit.color === 1){
            if(props.unit.isDestroyed){
                debugger;
                unitClass = classes.bluewreck;
            }
            else {
                unitClass = classes.bluetank;
            }
        }
    
        if(props.unit.color === 3){
            if(props.unit.isDestroyed){
                debugger;
                unitClass = classes.yellowwreck;
            }
            else {
                unitClass = classes.yellowtank;
            }
        }
    }

    const onClickHandler = () => {
        setIsPressed(prevState => !prevState);
    };

    return (
        <div onClick={onClickHandler} className={`${classes.tile} ${colClass} ${isPressed ? classes.pressed : ''} ${unitClass} ${props.obstacle ? classes.obstacle : ''} ${props.pickup ? classes.pickup : ''} ${props.revert ? classes.revert : ''}`}>
            {props.unit && <p className={classes.label}>{props.unit.label}</p>}
        </div>
    );
};

export default Tile;