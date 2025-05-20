import React from 'react'

export const Wait = () => {
  return (
    <div className='w-full min-h-screen flex items-center justify-center'>
        <div className="relative inline-block w-[50px] h-[50px]">
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
            <div className="absolute w-[2px] h-[2px] bg-blue-500 rounded-full animate-spin"></div>
        </div>
    </div>
  )
}
