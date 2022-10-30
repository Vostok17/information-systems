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
    const cellSize = Number(config.cellSize);
    const objectSize = Number(config.objectSize);
    const defaultPadding = (cellSize - objectSize) / 2;

    const [maze, setMaze] = useState([]);
    const [playerCoords, setPlayerCoords] = useState({
        x: defaultPadding,
        y: defaultPadding,
    });

    useEffect(() => {
        const loadMaze = async () => {
            const res = await axios.get(`${config.serverURL}/api/game`);
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

    const movePlayer = useCallback(
        direction => {
            const coordsChanges = {
                'up': (x, y) => {
                    return {
                        x: x,
                        y: y - cellSize,
                    }
                },
                'down': (x, y) => {
                    return {
                        x: x,
                        y: y + cellSize
                    }
                },
                'left': (x, y) => {
                    return {
                        x: x - cellSize,
                        y: y
                    }
                },
                'right': (x, y) => {
                    return {
                        x: x + cellSize,
                        y: y
                    }
                },
            }

            setPlayerCoords(coords => coordsChanges[direction](coords.x, coords.y))

        }, [cellSize]);

    useEffect(() => {
        
        movePlayer('right')

    }, [movePlayer])

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
