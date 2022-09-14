import Tile from "./Tile";

const Board = (props) => {
    return (
        <div>
            <h1>{props.userName}</h1>
            <Tile /><Tile /><Tile /><Tile />
        </div>
    );
};

export default Board;