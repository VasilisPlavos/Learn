import Image from "next/image"
import { ArrowUpRight } from "lucide-react"
import { ReactElement } from "react"

export default function Home() {
  return (
    <div className="
      mx-auto 
      flex flex-col 
      items-center 
      lg:flex-row lg:items-start lg:justify-center lg:max-w-[1728px]
      p-6
      xl:p-16
      pt-12
      "

    >
      {/* Left column - flexible on large screens, 380px on small screens */}
      <div className="w-[380px] lg:flex-1 p-2">
        {/* Profile section */}
        <div className="gap-8 mb-12">
          <div className="items-center md:items-start">
            <div className="relative w-32 h-32 md:w-48 md:h-48 rounded-full overflow-hidden mb-4">
              <Image src="/profile-image.jpg" alt="Profile" fill className="object-cover" priority />
            </div>
            <h1 className="text-[32px] font-bold leading-[120%] tracking-[-1px] xl:text-[44px] xl:tracking-[-2px]">Vasilis Plavos</h1>
            <p className="text-gray-700 max-w-md mt-3 xl:text-xl pr-10">
              I've put together this list of my favorite AI tools that I use every day. These tools make everything
              fast. Explore, try them out, and see how they can help with your projects!
            </p>
          </div>
        </div>
      </div>

      {/* Right column - fixed 820px on large screens, 380px on small screens */}
      <div className="w-[380px] lg:w-[820px]">
        {/* Text Assistants Section */}
        <div className="mb-12">
          <h2 className="text-xl font-semibold mb-4">Text Assistants</h2>
          <div className="grid grid-cols-1 xl:grid-cols-4 gap-8">
            <ToolCard
              name="ChatGPT"
              textColor="text-white"
              isWide={true}
              description="My favorite AI assistant for generating ideas, drafting content, and brainstorming solutions. A must-have for any creative professional."
              color="bg-[#6baa9f]">
              <svg viewBox="0 0 24 24" className="w-full h-full" fill="white">
                <path d="M22.2819 9.8211a5.9847 5.9847 0 0 0-.5157-4.9108 6.0462 6.0462 0 0 0-6.5098-2.9A6.0651 6.0651 0 0 0 4.9807 4.1818a5.9847 5.9847 0 0 0-3.9977 2.9 6.0462 6.0462 0 0 0 .7427 7.0966 5.98 5.98 0 0 0 .511 4.9107 6.051 6.051 0 0 0 6.5146 2.9001A5.9847 5.9847 0 0 0 13.2599 24a6.0557 6.0557 0 0 0 5.7718-4.2058 5.9894 5.9894 0 0 0 3.9977-2.9001 6.0557 6.0557 0 0 0-.7475-7.0729zm-9.022 12.6081a4.4755 4.4755 0 0 1-2.8764-1.0408l.1419-.0804 4.7783-2.7582a.7948.7948 0 0 0 .3927-.6813v-6.7369l2.02 1.1686a.071.071 0 0 1 .038.052v5.5826a4.504 4.504 0 0 1-4.4945 4.4944zm-9.6607-4.1254a4.4708 4.4708 0 0 1-.5346-3.0137l.142.0852 4.783 2.7582a.7712.7712 0 0 0 .7806 0l5.8428-3.3685v2.3324a.0804.0804 0 0 1-.0332.0615L9.74 19.9502a4.4992 4.4992 0 0 1-6.1408-1.6464zM2.3408 7.8956a4.485 4.485 0 0 1 2.3655-1.9728V11.6a.7664.7664 0 0 0 .3879.6765l5.8144 3.3543-2.0201 1.1685a.0757.0757 0 0 1-.071 0l-4.8303-2.7865A4.504 4.504 0 0 1 2.3408 7.872zm16.5963 3.8558L13.1038 8.364 15.1192 7.2a.0757.0757 0 0 1 .071 0l4.8303 2.7913a4.4944 4.4944 0 0 1-.6765 8.1042v-5.6772a.79.79 0 0 0-.407-.667zm2.0107-3.0231l-.142-.0852-4.7735-2.7818a.7759.7759 0 0 0-.7854 0L9.409 9.2297V6.8974a.0662.0662 0 0 1 .0284-.0615l4.8303-2.7866a4.4992 4.4992 0 0 1 6.6802 4.66zM8.3065 12.863l-2.02-1.1638a.0804.0804 0 0 1-.038-.0567V6.0742a4.4992 4.4992 0 0 1 7.3757-3.4537l-.142.0805L8.704 5.459a.7948.7948 0 0 0-.3927.6813zm1.0976-2.3654l2.602-1.4998 2.6069 1.4998v2.9994l-2.5974 1.5093-2.6067-1.4997z" />
              </svg>
            </ToolCard>

            <ToolCard
              name="Perplexity"
              description="A powerful research assistant, great for knowledge gathering."
              color="bg-[#0d2b33]"
              icon="🔍"
              textColor="text-white">
              <svg viewBox="0 0 24 24" className="w-full h-full" fill="none" stroke="#00c2ff" strokeWidth="1.5">
                <path d="M12 2L2 7L12 12L22 7L12 2Z" />
                <path d="M2 17L12 22L22 17" />
                <path d="M2 12L12 17L22 12" />
              </svg>
            </ToolCard>
            <ToolCard
              name="Claude"
              description="An assistant for writing, transforming, and expanding ideas."
              color="bg-[#e07a5f]"
              icon="☀️"
              textColor="text-white"
            />
          </div>
        </div>

        {/* Image Generators Section */}
        <div className="mb-12">
          <h2 className="text-xl font-semibold mb-4">Image Generators</h2>
          <div className="grid grid-cols-1 xl:grid-cols-3 gap-8">
            <ToolCard
              name="VisualElectric"
              description="Turn your ideas into cool visuals with this AI image generator!"
              color="bg-red-500"
              icon="⚡"
              textColor="text-white"
            />
            <ToolCard
              name="Ideogram"
              description="Stunning visuals with the ability to add custom text."
              color="bg-black"
              icon="🔠"
              textColor="text-white"
            />
            <ToolCard
              name="MidJourney"
              description="The gold standard for generating hyper-realistic visuals and conceptual art. This is my go-to AI tool for creative projects, and I use it most of the time."
              color="bg-gray-100"
              icon="🔱"
            />
            <ToolCard
              name="Krea AI"
              description="Love their Edit Tool, that is perfect for seamless adjustments."
              color="bg-white border border-gray-200"
              icon="🎨"
            />
            <ToolCard
              name="Recraft"
              description="Create your own custom style for generating consistent and stunning visuals. Perfect for brand designs that match your vision! Sign up using the link and earn 200 credits."
              color="bg-purple-500"
              icon="🔄"
              textColor="text-white"
              isWide={true}
            />
            <ToolCard
              name="Adobe Firefly"
              description="Great for different tasks, perfect for designers."
              color="bg-red-600"
              icon="🔥"
              textColor="text-white"
            />
          </div>
        </div>

        {/* Content Creation Section */}
        <div className="mb-12">
          <h2 className="text-xl font-semibold mb-4">Content Creation</h2>
          <div className="grid grid-cols-1 xl:grid-cols-3 gap-8">
            <ToolCard
              name="Chatplace"
              description="I use Chatplace to create content faster."
              color="bg-purple-200"
              icon="💬"
              isWide={false}
            />
            {/* Add more content creation tools as needed */}
          </div>
        </div>
      </div>
    </div>
  )
}

interface ToolCardProps {
  name: string
  description: string
  color: string
  icon?: string
  textColor?: string
  isWide?: boolean,
  children?: ReactElement
}

function ToolCard({
  name, description, color, icon, textColor = "text-black", isWide = false,
  children // Allow passing SVG as children
}: ToolCardProps) {
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

