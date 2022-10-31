import { useCallback, useEffect, useState } from 'react';
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
`;
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
    const [states, setStates] = useState([
        {
            player: { x: 0, y: 0 },
            enemy: { x: 0, y: 0 },
            finish: { x: 0, y: 0 },
        },
    ]);
    const [currState, setCurrState] = useState(0);

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
            setStates(res.data);
        };
        loadStates();
        console.log('GameStates loaded successfully');
    }, []);

    const handleKeyPress = useCallback(e => {
        setCurrState(state => {
            if (e.key === 'ArrowLeft') {
                return state - 1;
            }
            if (e.key === 'ArrowRight') {
                return state + 1;
            }
        });
    }, []);

    useEffect(() => {
        document.addEventListener('keydown', handleKeyPress);

        return () => {
            document.removeEventListener('keydown', handleKeyPress);
        };
    }, [handleKeyPress]);

    return (
        <Matrix>
            {(() => {
                if (currState < 0) {
                    setCurrState(0);
                }
                if (currState >= 0 && currState < states.length) {
                    return (
                        <>
                            <Player coords={states[currState].player} />
                            <Enemy coords={states[currState].enemy} />
                            <Finish coords={states[currState].finish} />
                        </>
                    );
                }
                if (currState >= states.length) {
                    setCurrState(Number(states.length) - 1);
                }
            })()}
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
