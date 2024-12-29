import React from 'react'
import { Link } from 'react-router-dom'
import 'axios'

const PlcStatusAndNav = ({ links }) => {

    return (
        <div className="d-flex justify-content-between">
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    {links.map((item, key) => {
                        const link = item.link[0] !== "/" ? `/${item.link}` : item.link;
                        const isLastItem = key === links.length - 1;
                        return (
                            <li key={key} className="breadcrumb-item false">
                                {isLastItem ? (
                                    item.title
                                ) : (
                                    <Link to={`${link}`}>{item.title}</Link>
                                )}
                            </li>
                        );
                    })}
                </ol>
            </nav>
            {/*<span>PlC Durumu : <span className={`fw-bold ${plcStatus ? "text-success":"text-danger"}`}>{plcStatus ? "Bağlantı Kuruldu":"Bağlantı Kurulamadı"}</span></span>*/}
        </div>
    )
}

export default PlcStatusAndNav