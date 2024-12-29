import React,{useState,useEffect} from 'react'
import { useLocation } from 'react-router-dom'
import PlcStatusAndNav from '../PlcStatusAndNav'
import PlcNowData from './PlcNowData'
import axios from 'axios'
import PlcLast50Data from './PlcLast50Data'


const PlcPageApp = () => {
    const [page, setPage] = useState(1) 
    const [currentPage, setCurrentPage] = useState("Anasayfa")
    const location = useLocation()
    const [resetErrors, setResetErrors] = useState(false)

    useEffect(() => {
        const params = new URLSearchParams(location.search)
        const pageParam = params.get('page')
        if (pageParam) {
            setPage(parseInt(pageParam, 10))
        } else {
            setPage(1)
        }
    }, [location.search])

    useEffect(()=>{
        axios.get('https://localhost:7189/api/getpages').then(res=>{
            res.data.forEach(item=>{
                if(item.page_id == page){
                    setCurrentPage(item.page_title)
                }
            })
        });
    },[page])


    return (
        <>
            <div className='col-12'>
                <div className='card border-0'>
                    <div className='card-body'>
                        <PlcStatusAndNav links={[{title:"İstasyonlar",link:"/"},{ title: currentPage, link: "/" }]} />
                        {<PlcNowData page={page} resetErrors={resetErrors} setResetErrors={setResetErrors} />}
                    </div>
                </div>
            </div>

           {<div className='col-12 mt-4'>
                <div className='card border-0'>
                    <div className='card-body'>
                        <div className='mb-3'><p className='font1 mb-1 '>Son 50 Kayıt</p><hr className='m-0 mb-2' width="100" /></div>
                        <PlcLast50Data page={page} resetErrors={resetErrors} setResetErrors={setResetErrors} />
                    </div>
                </div>
            </div>}


        </>
    )
}

export default PlcPageApp