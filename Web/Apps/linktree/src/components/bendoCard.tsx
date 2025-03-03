import { ArrowUpRight } from "lucide-react"
import { ReactElement } from "react"

export interface BendoCardProps {
  name: string
  description: string
  color: string
  icon?: string
  textColor?: string
  isWide?: boolean,
  children?: ReactElement
}

export function BendoCard({
    name, description, color, icon, textColor = "text-black", isWide = false,
    children // Allow passing SVG as children
  }: BendoCardProps) {
    return (
      <div className={`${color} ${isWide ? "xl:col-span-2" : ""}  ${textColor} rounded-2xl p-4 relative overflow-hidden`}>
        <div className="relative z-10">
          <div className="flex justify-between items-start">
            <div className="w-10 h-10">
              {children && children.type === "svg" ? (children) : (<div className="lg:text-2xl mb-2">{icon}</div>)}
            </div>
            <button className="bg-white/20 rounded-full p-1.5">
              <ArrowUpRight className={`w-4 h-4 ${textColor}`} />
            </button>
          </div>
  
          <h2 className="text-xl font-bold mt-6 mb-2">{name}</h2>
          <p className={`text-sm ${textColor}`}>{description}</p>
        </div>
        <div className="absolute top-0 right-0 opacity-10 text-[200px] font-bold leading-none">{name.charAt(0)}</div>
      </div>
    )
  }