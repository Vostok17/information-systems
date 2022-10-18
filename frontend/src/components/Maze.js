import { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import styled from 'styled-components';
import config from '../config.json';

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
    top: ${props => props.coords.y + 'px'};
    left: ${props => props.coords.x + 'px'};
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

function Maze() {
    const [maze, setMaze] = useState([]);
    const [playerCoords, setPlayerCoords] = useState({
        x: 5,
        y: 5,
    });

    useEffect(() => {
        const loadMaze = async () => {
            const res = await axios.get(`${config.serverURL}/api/game`);
            const maze = convertToMatrix(
                res.data.maze,
                res.data.height,
                res.data.width,
            );
            setMaze(maze);
        };
        loadMaze();
    }, []);

    const move = useCallback(
        direction => {
            const distance = Number(config.cellSize);

            switch (direction) {
                case 'up':
                    setPlayerCoords({
                        x: playerCoords.x,
                        y: playerCoords.y - distance,
                    });
                    break;
                case 'down':
                    setPlayerCoords({
                        x: playerCoords.x,
                        y: playerCoords.y + distance,
                    });
                    break;
                case 'left':
                    setPlayerCoords({
                        x: playerCoords.x - distance,
                        y: playerCoords.y,
                    });
                    break;
                case 'right':
                    setPlayerCoords({
                        x: playerCoords.x + distance,
                        y: playerCoords.y,
                    });
                    break;
                default:
                    break;
            }
        },
        [playerCoords.x, playerCoords.y],
    );

    const onKeyDown = useCallback(
        e => {
            switch (e.code) {
                case 'ArrowUp':
                    move('up');
                    break;
                case 'ArrowDown':
                    move('down');
                    break;
                case 'ArrowLeft':
                    move('left');
                    break;
                case 'ArrowRight':
                    move('right');
                    break;
                default:
                    console.log('Nothing!');
                    break;
            }
        },
        [move],
    );

    useEffect(() => {
        document.addEventListener('keydown', onKeyDown);
        return () => {
            document.removeEventListener('keydown', onKeyDown);
        };
    }, [onKeyDown]);

    return (
        <Matrix>
            <Player coords={playerCoords} />
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

export default Maze;
