import React from 'react'
import { FaArrowLeft } from "react-icons/fa";
import { Link } from 'react-router-dom'

const Error = () => {
    return (
        <div className='col-12'>
            <div className='card border-0'>
                <div className='card-body text-center'>
                    <h4>Aradığınız Sayfa Bulunamadı</h4>
                    <p>Aradığınız sayfa kaldırılmış veya değiştirilmiş lütfen daha sonra tekrar deneyin.</p>
                    <Link className='btn btn-danger d-inline-block'>
                        <FaArrowLeft className='me-3' />
                        Anasayfaya Dön
                    </Link>
                    <div><img src="/error2.png" className="img-fluid" /></div>
                </div>
            </div>
        </div>
    )
}

export default Error