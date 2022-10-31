import { useEffect, useState } from 'react';
import axios from 'axios';
import styled from 'styled-components';
import config from '../config.json';

const cellSize = Number(config.cellSize);
const objectSize = Number(config.objectSize);
const defaultPadding = (cellSize - objectSize) / 2;

const Matrix = styled.div`
    display: flex;
    flex-direction: column;
    position: relative;
`;
const Player = styled.div`
    background-color: green;
    border-radius: 50%;
    height: 30px;
    width: 30px;
    position: absolute;
    top: ${props => defaultPadding + props.coords.x * cellSize + 'px'};
    left: ${props => defaultPadding + props.coords.y * cellSize + 'px'};
`;
const Enemy = styled(Player)`
    background-color: black;
`;
const Finish = styled(Player)`
    background-color: brown;
`
const Cell = styled.div`
    background: ${props => (props.isWall ? 'crimson' : 'azure')};
    height: 40px;
    width: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
`;
const Row = styled.div`
    display: flex;
    flex-direction: row;
`;

function Game() {
    const [maze, setMaze] = useState([]);
    const [states, setStates] = useState([]);
    const [currState, setCurrState] = useState({
        player: { x: 0, y: 0 },
        enemy: { x: 0, y: 0 },
        finish: { x: 0, y: 0 },
    })

    useEffect(() => {
        const loadMaze = async () => {
            const res = await axios.get(`${config.serverURL}/api/game/maze`);
            const maze = convertToMatrix(
                res.data.maze,
                res.data.rows,
                res.data.cols,
            );
            setMaze(maze);
        };
        loadMaze();
        console.log('Maze loaded successfully');
    }, []);

    useEffect(() => {
        const loadStates = async () => {
            const res = await axios.get(`${config.serverURL}/api/game/states`);
            setStates(res.data)
            setCurrState(res.data[0])
        }
        loadStates();
        console.log('GameStates loaded successfully')
    }, []);

    return (
        <Matrix>
            <Player coords={currState.player} />
            <Enemy coords={currState.enemy} />
            <Finish coords={currState.finish} />
            {console.log(currState.player, currState.enemy, currState.finish)}
            {maze.map((row, i) => (
                <Row key={i}>
                    {row.map((col, j) => (
                        <Cell key={j} isWall={col === 0} />
                    ))}
                </Row>
            ))}
        </Matrix>
    );
}
function convertToMatrix(strToConvert, rows, cols) {
    let matrix = new Array(rows);
    for (let index = 0; index < rows; index++) {
        matrix[index] = new Array(cols);
    }

    const arr = strToConvert.split(' ');

    for (let i = 0; i < rows; i++) {
        for (let j = 0; j < cols; j++) {
            matrix[i][j] = Number(arr[i * cols + j]);
        }
    }

    return matrix;
}

export default Game;
