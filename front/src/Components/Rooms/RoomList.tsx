﻿import React from "react";
import {RoomItem} from "./RoomItem";
import {Room} from "../../Models/Room";
import {CircularProgress} from "@mui/material";
import {useRoomsQuery} from "../../ApiHooks/roomsApiHooks";
import {AddRoomButton} from "./AddRoomButton";
import {CircularProgressStyle} from "../../Styles/SxStyles";


export const RoomList: React.FC = () => {
    const {data: rooms, isLoading} = useRoomsQuery()

    return <div
        style={{
            padding: '16px',
            margin: "0px",
            display: 'flex',
            flexDirection: 'column',
            backgroundColor: '#FFF',
            gap: '10px'
        }}>

        {isLoading ? <CircularProgress sx={CircularProgressStyle}/> : rooms!.map((room: Room) =>
            <RoomItem key={room.id} room={room}/>)
        }
        {!isLoading && <AddRoomButton key={"add-button-key"}/>}

    </div>
}