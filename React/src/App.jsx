import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import Header from './components/Header'
import { Routes, Route } from 'react-router-dom'
import Error from './components/404/Error'
import PlcPageApp from './components/PlcPage/PlcPageApp'
import FilterApp from './components/FilterApp'
import { FaHeart } from "react-icons/fa6";  
import { Toaster } from 'react-hot-toast';

function App() {

  return (
    <>
      <Toaster/>
      <Header />
      <div className='container-fluid my-4'>
        <div className='row'>
          <Routes>
            <Route path='/' element={<PlcPageApp />}></Route>
            {<Route path='/filter' element={<FilterApp />}></Route>}
            <Route path='/*' element={<Error />}></Route>
          </Routes>
        </div>
      </div>
      <div className='footer text-center my-4'>
        <small>Copyright @2024 OMG Elektrik <FaHeart className='text-danger' /> Coding By <a href="https://www.yazilimajansi.net">Yazılım Ajansı</a></small>
      </div>
    </>
  )
}

export default App
