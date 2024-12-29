import React, { useState } from 'react'
import {NavLink,Link} from 'react-router-dom'
import { FcElectricity } from "react-icons/fc";
import axios from 'axios';

const Header = () => {

    const [pages, setPages] = useState([])
    axios.get('https://localhost:7189/api/getpages').then(res=>{
        setPages(res.data)
    });


    return (
        <div className="container-fluid bg-white shadow py-3">
            <div className="container-fluid">
                <nav className="navbar navbar-expand-lg navbar-light bg-white">
                    <Link className="navbar-brand font1" to="/"><FcElectricity /> OMG Elektrik</Link>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav mr-auto d-flex w-100 justify-content-end font1">
                            {/*<li className="nav-item active">
                                <NavLink to="/" className="nav-link">PLC Anlık Veriler</NavLink>
                            </li> */}
                            {pages.map(item=>(
                                <li key={item.page_id} className="nav-item">
                                    <Link to={`/?page=${item.page_id}`} className="nav-link">{item.page_title}</Link>
                                </li>
                            ))}
                            <li className="nav-item active">
                                <Link to="/filter" className="nav-link">PLC Verilerini Filtrele</Link>
                            </li>
                        </ul>
                    </div>
                </nav>
            </div>
        </div>
    )
}

export default Header