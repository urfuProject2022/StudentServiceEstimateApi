import React, {useState} from 'react';
import '../../App.css';
import {Button, Card, FormControl, TextField} from "@mui/material";
import {useNavigate} from "react-router-dom";
import {useAuth} from "../ProtectedRoutes/AuthProvider";
import {LoginRequest} from  "../../Utils/Requests"
import "../../Styles/Auth.css"

const Login = () => {
    const [login, setLogin] = useState("")
    const [password, setPassword] = useState("")
    const [errorMessage, setErrorMessage] = useState("")

    const auth = useAuth()
    const navigate = useNavigate()

    const onLogin = () => {
        LoginRequest(login, password)
            .then(async resp => {
                if (!resp.ok){
                    let errorMessage = await resp.text()
                    setErrorMessage(errorMessage)
                }
                else {
                    auth.setCookieState(true)
                    navigate("/")
                }
            })
    }

    return <div className="auth-container">
        <Card variant={"outlined"} className="auth-card" sx={{background: "#FAFAFA"}}>
            <FormControl margin={"normal"}
                         className="auth">
                <TextField label="Логин"
                           autoFocus={true}
                           onChange={x => setLogin(x.target.value)}
                           id={"login"}/>
                <TextField label="Пароль"
                           type="password"
                           onChange={x => setPassword(x.target.value)}
                           id={"password"}/>
                <Button onClick={() => onLogin()}
                        variant="contained"
                        disabled={!login || !password}>Войти</Button>
                <Button variant={"outlined"}
                        onClick={() => navigate("/registration")}
                        type="submit">Регистрация</Button>
            </FormControl>
            <div className="error">{errorMessage}</div>
        </Card>
    </div>
}

export default Login;