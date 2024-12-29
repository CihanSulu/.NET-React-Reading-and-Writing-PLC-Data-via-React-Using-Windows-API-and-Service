import React, { useState, useEffect } from 'react'
import FilterTableSql from '../Filter/FilterTableSQL'
import axios from 'axios'
import DownloadTable from './DownloadTable'


const FilterElements = () => {

    const [pages, setPages] = useState([])
    const [selectPage, setSelectPage] = useState(1)
    const [date1, setDate1] = useState("")
    const [date2, setDate2] = useState("")
    const [number1, setNumber1] = useState(0)
    const [number2, setNumber2] = useState(99999)
    const [datas, setDatas] = useState([])

    useEffect(() => {
        axios.get('https://localhost:7189/api/getpages').then(res => {
            setPages(res.data)
        });

        const today = new Date();
        const formattedDate1 = today.toISOString().split('T')[0] + "T00:00"; // YYYY-MM-DDTHH:00
        const formattedDate2 = today.toISOString().split('T')[0] + "T23:59"; // YYYY-MM-DDTHH:59
        setDate1(formattedDate1);
        setDate2(formattedDate2);
    }, []);

    const handlePageSelect = (e) => {
        setSelectPage(e.target.value);
    }
    const handleDate1Change = (e) => {
        setDate1(e.target.value);
    };
    const handleDate2Change = (e) => {
        setDate2(e.target.value);
    };
    const handleNumber1Change = (e) => {
        setNumber1(e.target.value)
    };
    const handleNumber2Change = (e) => {
        setNumber2(e.target.value)
    };

    useEffect(() => {
        if (date1 && date2) {
            axios.post('https://localhost:7189/api/getsqldata', {
                page: selectPage,
                last50: false,
                date1: date1,
                date2: date2,
                number1: number1,
                number2: number2
            }).then(res => {
                setDatas(res.data)
            })
        }
    }, [date1, date2, selectPage, number1, number2]);


    return (
        <div className='row'>
            <div className='col-12 d-flex gap-2 justify-content-between'>

                <div className='d-flex gap-2'>
                    <label className='my-auto mx-auto' style={{width:"81px"}}>Tarih:</label>
                    <input type="datetime-local" value={date1} style={{width:"194px"}} onChange={handleDate1Change} className='form-control'/>
                    <span className='my-auto'>-</span>
                    <input type="datetime-local" value={date2} style={{width:"194px"}} onChange={handleDate2Change} className='form-control'/>
                </div>

                <div className='d-flex gap-2 testx'>
                    <select className='form-control' style={{width:"120px"}} value={selectPage}onChange={handlePageSelect}>
                        {pages.map(item => (
                            <option key={item.page_id} value={item.page_id}>
                                {item.page_title}
                            </option>
                        ))}
                    </select>
                    <DownloadTable data={datas}/>
                </div>
            </div>

            <div className='col-12 d-flex gap-2 justify-content-between mt-3'>
                <div className='d-flex gap-2'>
                    <label className='my-auto mx-auto'>Döküm No:</label>
                    <input type="number" style={{width:"194px"}} value={number1} onChange={handleNumber1Change} className='form-control'/>
                    <span className='my-auto'>-</span>
                    <input type="number" style={{width:"194px"}} value={number2} onChange={handleNumber2Change} className='form-control'/>
                </div>
            </div>

            <div className='col-12 mt-2'>
                {datas.length == 0 ? <div>Veri Bulunamadı</div>:<FilterTableSql data={datas} page={selectPage} />}
            </div>

        </div>
    )
}

export default FilterElements
